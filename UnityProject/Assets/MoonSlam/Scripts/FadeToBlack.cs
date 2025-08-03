using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public static FadeToBlack Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        Instance = this;
    }

    [SerializeField] private Image _fullscreenPanel;

    private bool _fading;
    private float _fadeTimeStart;
    private float _fadeTimeEnd;

    private void Start()
    {
        _fullscreenPanel.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (_fading)
        {
            float lerp = Mathf.InverseLerp(_fadeTimeStart, _fadeTimeEnd, Time.time);
            if (!_fadeOut)
            {
                lerp = 1 - lerp;
            }
            Color color = Color.black;
            color.a = lerp;
            _fullscreenPanel.color = color;
            
            if (Time.time > _fadeTimeEnd)
            {
                if (_fadeOut)
                {
                    _fullscreenPanel.color = Color.black;
                }
                else
                {
                    _fullscreenPanel.gameObject.SetActive(false);
                    
                }

                _fading = false;
            }
        }
    }
    
    private bool _fadeOut = true;

    public void FadeOut(float time)
    {
        _fadeOut = true;
        _fullscreenPanel.gameObject.SetActive(true);
        _fadeTimeStart = Time.time;
        _fadeTimeEnd = Time.time + time;
        _fading = true;
    }
    
    public void FadeIn(float time)
    {
        _fadeOut = false;
        _fullscreenPanel.gameObject.SetActive(true);
        _fadeTimeStart = Time.time;
        _fadeTimeEnd = Time.time + time;
        _fading = true;
    }
}
