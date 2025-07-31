using System;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _moon;

    private void Update()
    {
        Vector3 newPosition = Vector3.Lerp(_startPoint.position, _endPoint.position, GameManager.Instance.TimeRemainingNormalized);
        _moon.position = newPosition;

    }
}
