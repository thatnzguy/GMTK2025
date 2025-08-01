using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _defaultDayDurationSeconds;
    [SerializeField] private GameObject _dayEndScreen;
    [SerializeField] private float _endScreenDuration = 5;
    [SerializeField] private SceneAsset _endScreen;
    
    public int TimeRemainingSeconds;
    public float TimeRemainingNormalized;
    
    public float _dayEndTime;
    public float _dayDuration;
    public float _dayStartTime;
    private bool _dayEnded;
    public UnityEvent OnBeginDay;
    public UnityEvent OnEndDay;

    public static GameManager Instance;

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
        
        _dayEndScreen.SetActive(false);

        string sceneName = SceneManager.GetActiveScene().name;
        
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => loadSceneAsync.isDone);
        
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

    public void EngineActivated()
    {
        SceneManager.LoadScene(_endScreen.name);
    }
}
