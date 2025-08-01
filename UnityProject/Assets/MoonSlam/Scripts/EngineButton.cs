using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EngineButton : Interactable
{
    [SerializeField] private Color _readyColor = Color.green;
    [SerializeField] private Color _notReadyColor = Color.red;
    
    private AttachPoint[] _attachPoints;

    private bool _ready;

    private void Start()
    {
        _outline.OutlineColor = _notReadyColor;
        _attachPoints = transform.parent.GetComponentsInChildren<AttachPoint>();
        foreach (AttachPoint attachPoint in _attachPoints)
        {
            attachPoint.onInteract.AddListener(AttachPointOnAttached);
        }
    }

    private void AttachPointOnAttached()
    {
        int numAttached = _attachPoints.Count(attachPoint => attachPoint.IsAttached);
        if (numAttached == _attachPoints.Length)
        {
            _ready = true;
            _outline.OutlineColor = _readyColor;
        }
    }

    public override void Interact(Interactor interactor)
    {
        if (_ready)
        {
            GameManager.Instance.EngineActivated();
            base.Interact(interactor);
        }
    }
}
