using System;
using UnityEngine;



public interface IInteractable
{
    public bool CanFocus(Interactor interactor);

    public void FocusOn();
    public void FocusOff();

    // public void CanInteract();
    public void Interact(Interactor interactor);
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _maxInteractDistance = 10;
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _throwForce = 1;
    [SerializeField] private Rigidbody _rigidbody;

    private RaycastHit[] _raycastHits = new RaycastHit[100];

    public Pickupable HeldItem { get; private set; }
    private IInteractable _focusedInteractable;

    private void Update()
    {
        //Focus
        Ray ray = new Ray(transform.position, transform.forward);
        int numHits = Physics.RaycastNonAlloc(ray, _raycastHits, _maxInteractDistance);
        Collider hitCollider = null;
        bool focused = false;
        if (numHits > 0)
        {
            for (var i = 0; i < numHits; i++)
            {
                RaycastHit raycastHit = _raycastHits[i];
                IInteractable interactable = raycastHit.collider.GetComponentInParent<IInteractable>();
                if (interactable != null && interactable.CanFocus(this))
                {
                    focused = true;
                    hitCollider = raycastHit.collider;

                    if (_focusedInteractable != null)
                    {
                        _focusedInteractable.FocusOff();
                        _focusedInteractable = null;
                    }

                    _focusedInteractable = interactable;
                    _focusedInteractable.FocusOn();
                    break;
                }
            }
        }

        if (!focused && _focusedInteractable != null)
        {
            _focusedInteractable.FocusOff();
            _focusedInteractable = null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_focusedInteractable != null)
            {
                //TODO could be refactored to be like the AttachPoint, and do a thing for you
                Pickupable pickupable = hitCollider.GetComponentInParent<Pickupable>();
                if (pickupable != null)
                {
                    if (HeldItem != null)
                    {
                        DropHeldItem();
                    }
                    HeldItem = pickupable;
                }
                _focusedInteractable.Interact(this);
            } 
            else if (HeldItem)
            {
                ThrowHeldItem();
            }
        }
    }

    private void LateUpdate()
    {
        if (HeldItem != null)
        {
            HeldItem.transform.SetPositionAndRotation(_holdPosition.position, _holdPosition.rotation);
        }
    }

    public void ReleaseHeldItem()
    {
        HeldItem = null;
    }

    private void DropHeldItem()
    {
        Vector3 force = _rigidbody.linearVelocity;
        HeldItem.Drop(force);
        HeldItem = null;
    }

    private void ThrowHeldItem()
    {
        Vector3 force = _rigidbody.linearVelocity + _holdPosition.forward * _throwForce;
        HeldItem.Drop(force);
        HeldItem = null;
    }
}