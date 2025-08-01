using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawn;
    [SerializeField] private EngineButton _engineButton;

    public UnityEvent OnEngineActivated;
    private AttachPoint[] _attachPoints;

    public Transform PlayerSpawn => _playerSpawn;

    private void Start()
    {
        _engineButton.onInteract.AddListener(EngineActivated);
        _attachPoints = GetComponentsInChildren<AttachPoint>();
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
            _engineButton.EnableActivate();
        }
    }
    private void EngineActivated()
    {
        //TODO leave effect
        OnEngineActivated?.Invoke();
    }
}
