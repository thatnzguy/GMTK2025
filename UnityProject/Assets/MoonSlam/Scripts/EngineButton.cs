using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EngineButton : Interactable
{
    private AttachPoint[] _attachPoints;
    public UnityEvent OnEngineActivate;

    private void Start()
    {
        _attachPoints = transform.parent.GetComponentsInChildren<AttachPoint>();
        foreach (AttachPoint attachPoint in _attachPoints)
        {
            attachPoint.OnAttached.AddListener(AttachPointOnAttached);
        }
    }

    private void AttachPointOnAttached()
    {
        int numAttached = _attachPoints.Count(attachPoint => attachPoint.IsAttached);
        if (numAttached == _attachPoints.Length)
        {
            OnEngineActivate?.Invoke();
            GameManager.Instance.EngineActivated();
        }
    }
}
