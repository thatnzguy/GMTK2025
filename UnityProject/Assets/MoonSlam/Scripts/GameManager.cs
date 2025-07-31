using System;
using CoreUtils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float _defaultDayDurationSeconds;

    public int TimeRemainingSeconds;
    public float TimeRemainingNormalized;
    
    private float _dayEndTime;
    private float _dayDuration;
    private float _dayStartTime;
    
    private void Start()
    {
        _dayDuration = _defaultDayDurationSeconds;
        BeginDay();
    }

    private void BeginDay()
    {
        _dayEndTime = Time.time + _dayDuration;
        _dayStartTime = Time.time;
    }

    private void Update()
    {
        TimeRemainingNormalized = Mathf.InverseLerp(_dayStartTime, _dayEndTime, Time.time);
        TimeRemainingSeconds = (int)(_dayEndTime - Time.time);
    }
}
