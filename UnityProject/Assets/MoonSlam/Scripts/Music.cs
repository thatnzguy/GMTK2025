using System;
using UnityEngine;
using UnityEngine.Events;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _loop;
    [SerializeField] private AudioClip _end;
    private float _startEndClipTime;
    private bool _endClipPlaying;

    private void Start()
    {
        GameManager.Instance.OnBeginDay.AddListener(BeginDay);
        GameManager.Instance.OnEndDay.AddListener(EndDay);
    }

    private void BeginDay()
    {
        _startEndClipTime = GameManager.Instance._dayEndTime - _end.length;
        _endClipPlaying = false;
        _audioSource.clip = _loop;
        _audioSource.Stop();
        _audioSource.Play();
    }

    private void EndDay()
    {
        _audioSource.Stop();
    }

    private void Update()
    {
        if (!_endClipPlaying && Time.time > _startEndClipTime)
        {
            _endClipPlaying = true;
            _audioSource.clip = _end;
            _audioSource.Stop();
            _audioSource.Play();
        }
    }
}
