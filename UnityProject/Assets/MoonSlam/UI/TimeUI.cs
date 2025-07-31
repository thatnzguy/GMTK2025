using System;
using TMPro;
using UnityEngine;

public class TimeDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        string text = GameManager.Instance.TimeRemainingSeconds.ToString();
        _text.text = text;
    }
}
