using System;
using CoreUtils;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private UDateTime _worldTimeEnd;
    [SerializeField] private float _timeMultiplier = 10;
    [SerializeField] private float _defaultDayDurationSeconds;

    public DateTime CurrentClockTime;
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
        CurrentClockTime = _worldTimeEnd.dateTime.AddSeconds(-_dayDuration * _timeMultiplier);
    }

    private void Update()
    {
        TimeRemainingNormalized = Mathf.InverseLerp(_dayStartTime, _dayEndTime, Time.time);
        
        float timeSinceDayStart = Time.time - _dayStartTime;
        float timeRemaining = (_dayDuration * _timeMultiplier) - timeSinceDayStart;
        
        CurrentClockTime = _worldTimeEnd.dateTime.AddSeconds(-timeRemaining);
    }
}
