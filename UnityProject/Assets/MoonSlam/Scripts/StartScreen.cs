using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private String _gameScene;
    [SerializeField] private float _fadeInTime = 3;
    
    
    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartClick);
    }
    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartClick);
    }

    private void OnStartClick()
    {
        StartCoroutine(nameof(BeginGame));
    }

    private IEnumerator BeginGame()
    {
        FadeToBlack.Instance.FadeOut(_fadeInTime);
        
        yield return new WaitForSeconds(_fadeInTime);
        
        SceneManager.LoadScene(_gameScene);
    }
}
