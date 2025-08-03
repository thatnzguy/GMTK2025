using System;
using UnityEngine;

public class Moon : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _moon;
    [SerializeField] private AnimationCurve _moveCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.Linear(0,0,1,1);
    [SerializeField] private float _endScale;

    private void Update()
    {
        float curvedLerp = _moveCurve.Evaluate(GameManager.Instance.TimeRemainingNormalized);
        Vector3 newPosition = Vector3.Lerp(_startPoint.position, _endPoint.position, curvedLerp);
        _moon.position = newPosition;
        
        float scaleScale = _scaleCurve.Evaluate(GameManager.Instance.TimeRemainingNormalized);
        float scaleLerp = Mathf.Lerp(1, _endScale, scaleScale);
        Vector3 scale = new Vector3(scaleLerp, scaleLerp, scaleLerp);
        _moon.localScale = scale;
    }
}
