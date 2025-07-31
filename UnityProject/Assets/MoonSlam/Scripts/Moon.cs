using System;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _moon;
    [SerializeField] private AnimationCurve _moveCurve = AnimationCurve.Linear(0,0,1,1);
    

    private void Update()
    {
        float curvedLerp = _moveCurve.Evaluate(GameManager.Instance.TimeRemainingNormalized);
        Vector3 newPosition = Vector3.Lerp(_startPoint.position, _endPoint.position, curvedLerp);
        _moon.position = newPosition;

    }
}
