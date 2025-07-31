using System;
using UnityEngine;



public interface IInteractable
{
    public void FocusOn();
    public void FocusOff();
    
    //Return true if holdable
    public bool Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _maxInteractDistance = 10;
    [SerializeField] private Transform _holdPosition;

    private RaycastHit[] _raycastHits = new RaycastHit[100];

    private PickupItem _heldItem;
    private IInteractable _focusedInteractable;
    
    private void Update()
    {
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
                if (interactable != null)
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
                if (_focusedInteractable.Interact())
                {
                    PickupItem pickupItem = hitCollider.GetComponentInParent<PickupItem>();
                    if (pickupItem != null)
                    {
                        if (_heldItem != null)
                        {
                            DropHeldItem();
                        }

                        Pickup(pickupItem);
                    }
                }
            } 
            else if (_heldItem)
            {
                DropHeldItem();
            }
        }
    }

    private void LateUpdate()
    {
        if (_heldItem != null)
        {
            _heldItem.transform.SetLocalPositionAndRotation(_holdPosition.position, _holdPosition.rotation);
        }
    }

    private void Pickup(PickupItem item)
    {
        _heldItem = item;
        _heldItem.Pickup(this);
        Debug.Log("Pickup");
    }

    private void DropHeldItem()
    {
        //TODO throw
        _heldItem.Drop();
        _heldItem = null;
        Debug.Log("DropHeldItem");
    }
}