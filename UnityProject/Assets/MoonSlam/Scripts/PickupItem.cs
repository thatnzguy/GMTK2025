using System;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Outline _outline;

    private void Start()
    {
        FocusOff();
    }

    public void FocusOn()
    {
        _outline.enabled = true;
    }

    public void FocusOff()
    {
        _outline.enabled = false;
    }

    public bool Interact()
    {
        return true;
    }

    public void Pickup(Interactor interactor)
    {
        _rigidbody.isKinematic = true;

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void Drop()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        
        _rigidbody.isKinematic = false;
    }
}
