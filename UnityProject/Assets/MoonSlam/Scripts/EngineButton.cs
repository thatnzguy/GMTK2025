using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EngineButton : Interactable
{
    private AttachPoint[] _attachPoints;

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
    }

    public override void Interact(Interactor interactor)
    {
        int numAttached = _attachPoints.Count(attachPoint => attachPoint.IsAttached);
        if (numAttached == _attachPoints.Length)
        {
            GameManager.Instance.EngineActivated();
            base.Interact(interactor);
        }
    }
}
