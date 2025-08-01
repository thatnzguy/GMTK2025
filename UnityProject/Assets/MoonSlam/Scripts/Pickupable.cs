using System;
using UnityEngine;

public class Pickupable : Interactable
{
    [SerializeField] protected Rigidbody _rigidbody;
    
    public override bool Interact(Interactor interactor)
    {
        return true;
    }

    protected bool _isHeld;
    protected Interactor _holdingInteractor;

    public void Pickup(Interactor interactor)
    {
        _holdingInteractor = interactor;
        _isHeld = true;
        _rigidbody.isKinematic = true;

        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    public void Drop()
    {
        _isHeld = false;
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        
        _rigidbody.isKinematic = false;
    }
}
