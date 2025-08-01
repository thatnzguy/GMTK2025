using System;
using UnityEngine;



public interface IInteractable
{
    public bool CanFocus(Interactor interactor);

    public void FocusOn();
    public void FocusOff();
    
    //Return true if holdable
    public bool Interact(Interactor interactor);
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _maxInteractDistance = 10;
    [SerializeField] private Transform _holdPosition;

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
                if (_focusedInteractable.Interact(this))
                {
                    Pickupable pickupable = hitCollider.GetComponentInParent<Pickupable>();
                    if (pickupable != null)
                    {
                        if (HeldItem != null)
                        {
                            DropHeldItem();
                        }

                        Pickup(pickupable);
                    }
                }
            } 
            else if (HeldItem)
            {
                DropHeldItem();
            }
        }
    }

    private void LateUpdate()
    {
        if (HeldItem != null)
        {
            HeldItem.transform.SetLocalPositionAndRotation(_holdPosition.position, _holdPosition.rotation);
        }
    }

    private void Pickup(Pickupable item)
    {
        HeldItem = item;
        HeldItem.Pickup(this);
        Debug.Log("Pickup");
    }

    public void ReleaseHeldItem()
    {
        HeldItem = null;
    }

    private void DropHeldItem()
    {
        //TODO throw
        HeldItem.Drop();
        HeldItem = null;
        Debug.Log("DropHeldItem");
    }
}