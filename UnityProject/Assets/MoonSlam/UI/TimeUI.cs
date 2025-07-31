using System;
using TMPro;
using UnityEngine;

public class TimeDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Update()
    {
        DateTime currentClockTime = GameManager.Instance.CurrentClockTime;
        string text = currentClockTime.ToShortTimeString();
        _text.text = text;
    }
}
