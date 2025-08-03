using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _defaultDayDurationSeconds;
    [SerializeField] private GameObject _dayEndScreen;
    [SerializeField] private float _endScreenDuration = 5;
    [SerializeField] private string _endScreen;
    [SerializeField] private string _gameScene;

    [SerializeField] private float _fadeInTime = 5;
    [SerializeField] private float _fadeOutTime = 2;

    [SerializeField] private AudioSource _VOSource;
    [SerializeField] private AudioClip _Intro;
    [SerializeField] private AudioClip _Intro2;
    [SerializeField] private AudioClip _failure1;
    [SerializeField] private AudioClip _failure2;
    [SerializeField] private AudioClip _failure3;
    [SerializeField] private AudioClip _midwayTip;
    [SerializeField] private float _midwayTipTime = 60f;
    [SerializeField] private AudioClip _success;

    public float TimeElapsedSeconds;
    public float TimeRemainingSeconds;
    public float TimeRemainingNormalized;
    
    public float _dayEndTime;
    public float _dayDuration;
    public float _dayStartTime;
    private bool _dayEnded;
    public UnityEvent OnBeginDay;
    public UnityEvent OnEndDay;

    public static GameManager Instance;

    public int DayNumber { get; private set; } = 1;
    public int LastSpawnIndex;

    public float _TimeOffset = 0;
    public float GameTime => Time.time + _TimeOffset;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        _dayEndScreen.SetActive(false);
        _dayDuration = _defaultDayDurationSeconds;
        BeginDay();
    }

    private void BeginDay()
    {

        switch (DayNumber)
        {
            case 1:
                _VOSource.clip = _Intro;
                _VOSource.PlayDelayed(3);
                break;
            case 2:
                _VOSource.clip = _Intro2;
                _VOSource.PlayDelayed(10);
                break;
        }
        
        FadeToBlack.Instance.FadeIn(_fadeInTime);
        _dayEndScreen.SetActive(false);
        _dayEndTime = GameTime + _dayDuration;
        _dayStartTime = GameTime;
        OnBeginDay?.Invoke();
    }

    private IEnumerator EndDay()
    {
        //TODO disable player input
        OnEndDay?.Invoke();
        _dayEndScreen.SetActive(true);

        switch (DayNumber)
        {
            case 1:
                _VOSource.PlayOneShot(_failure1);
                break;
            case 2:
                _VOSource.PlayOneShot(_failure2);
                break;
            case 3:
                _VOSource.PlayOneShot(_failure3);
                break;
        }
        
        yield return new WaitForSeconds(_endScreenDuration);
        
        FadeToBlack.Instance.FadeOut(_fadeOutTime);
        
        yield return new WaitForSeconds(3);
        
        DayNumber++;

        //Reload game scene
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(_gameScene);
        yield return new WaitUntil(() => loadSceneAsync.isDone);
        
        _dayEndScreen.SetActive(false);
        
        _dayEnded = false;
        BeginDay();
    }

    private void Update()
    {
        if (!_dayEnded && GameTime > _dayEndTime)
        {
            _dayEnded = true;
            StartCoroutine(nameof(EndDay));
            return;
        }

        if (DayNumber == 1 && !_midwayTipPlayed && GameTime > _midwayTipTime)
        {
            _midwayTipPlayed = true;
            _VOSource.PlayOneShot(_midwayTip);
        }
        
        TimeRemainingNormalized = Mathf.InverseLerp(_dayStartTime, _dayEndTime, GameTime);
        TimeRemainingSeconds = _dayEndTime - GameTime;
        TimeElapsedSeconds = GameTime - _dayStartTime;

        if (Input.GetKeyDown(KeyCode.Equals))
        {
            _TimeOffset += 10;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public bool[] ActivatedShips = new bool[3];
    private bool _midwayTipPlayed;

    public void EngineActivated(int index)
    {
        ActivatedShips[index] = true;
        bool allActivated = true;
        for (int i = 0; i < ActivatedShips.Length && i < DayNumber; i++)
        {
            if (ActivatedShips[i] == false)
            {
                allActivated = false;
                break;
            }
        }

        if (allActivated)
        {
            _VOSource.PlayOneShot(_success);
            SceneManager.LoadScene(_endScreen);
        }
    }
}
