using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EngineButton : Interactable
{
    [SerializeField] private Color _readyColor = Color.green;
    [SerializeField] private Color _notReadyColor = Color.red;
    
    private bool _ready;

    private void Start()
    {
        _outline.OutlineColor = _notReadyColor;
    }

    public void EnableActivate()
    {
        _ready = true;
        _outline.OutlineColor = _readyColor;
    }

    public override void Interact(Interactor interactor)
    {
        if (_ready)
        {
            base.Interact(interactor);
        }
    }
}
