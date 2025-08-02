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
    
    public int TimeRemainingSeconds;
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

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _dayEndScreen.SetActive(false);
        DontDestroyOnLoad(gameObject);
        _dayDuration = _defaultDayDurationSeconds;
        BeginDay();
    }

    private void BeginDay()
    {
        //TODO Add day# screen
        _dayEndScreen.SetActive(false);
        _dayEndTime = Time.time + _dayDuration;
        _dayStartTime = Time.time;
        OnBeginDay?.Invoke();
    }

    private IEnumerator EndDay()
    {
        //TODO disable player input
        OnEndDay?.Invoke();
        _dayEndScreen.SetActive(true);
        
        yield return new WaitForSeconds(_endScreenDuration);
        
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
        if (!_dayEnded && Time.time > _dayEndTime)
        {
            _dayEnded = true;
            StartCoroutine(nameof(EndDay));
            return;
        }
        
        TimeRemainingNormalized = Mathf.InverseLerp(_dayStartTime, _dayEndTime, Time.time);
        TimeRemainingSeconds = (int)(_dayEndTime - Time.time);
    }

    public bool[] ActivatedShips = new bool[3];

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
            SceneManager.LoadScene(_endScreen);
        }
    }
}
