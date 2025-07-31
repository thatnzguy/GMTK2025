using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private SceneAsset _gameScene;
    
    private void Awake()
    {
        _startButton.onClick.AddListener(OnStartClick);
    }

    private void OnStartClick()
    {
        SceneManager.LoadScene(_gameScene.name);
    }
}
