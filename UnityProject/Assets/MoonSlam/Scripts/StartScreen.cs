using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private String _gameScene;
    
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
        SceneManager.LoadScene(_gameScene);
    }
}
