using System;
using System.Collections;
using System.Threading.Tasks;
using CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _defaultDayDurationSeconds;
    [SerializeField] private GameObject _dayEndScreen;
    [SerializeField] private float _endScreenDuration = 5;
    
    public int TimeRemainingSeconds;
    public float TimeRemainingNormalized;
    
    private float _dayEndTime;
    private float _dayDuration;
    private float _dayStartTime;
    private bool _dayEnded;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instance = this;
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

    private IEnumerator EndDay()
    {
        //TODO disable player input
        _dayEndScreen.SetActive(true);
        
        yield return new WaitForSeconds(_endScreenDuration);
        
        _dayEndScreen.SetActive(false);

        string sceneName = SceneManager.GetActiveScene().name;
        
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => loadSceneAsync.isDone);
        
        _dayEnded = false;
        BeginDay();
    }
}
