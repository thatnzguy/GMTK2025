using System;
using UnityEngine;
using UnityEngine.Events;

public class AttachPoint : Interactable
{
    [SerializeField] private Color _focusColor;
    [SerializeField] private float _attachRadius = 0.5f;
    private Collider[] _overlapColliders = new Collider[10];

    private Attachable _attached;
    public bool IsAttached => _attached != null;

    private void Update()
    {
        int numOverlaps = Physics.OverlapSphereNonAlloc(transform.position, _attachRadius, _overlapColliders);
        if (_attached == null && numOverlaps > 0)
        {
            for (int index = 0; index <numOverlaps; index++)
            {
                Collider overlapCollider = _overlapColliders[index];
                Attachable attachable = overlapCollider.GetComponentInParent<Attachable>();
                if (attachable != null)
                {
                    Debug.Log("Attaching");
                    attachable.Attach(this);
                    _attached = attachable;
                    break;
                }
            }
        }
    }

    public override bool CanFocus(Interactor interactor)
    {
        return interactor.HeldItem != null && _attached == null;
    }

    public override void FocusOn()
    {
        //TODO IDK why this doesn't change the color
        _outline.OutlineColor = _focusColor;
    }

    public override void FocusOff()
    {
        _outline.OutlineColor = Color.white;
    }

    public override void Interact(Interactor interactor)
    {
        if (interactor.HeldItem != null)
        {
            Attachable attachable = interactor.HeldItem.GetComponent<Attachable>();
            if (attachable != null)
            {
                Attach(attachable);
                base.Interact(interactor);
            }
        }
    }

    private void Attach(Attachable attachable)
    {
        attachable.Attach(this);
        _attached = attachable;
        _outline.enabled = false;
        FocusOff();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attachRadius);
    }
}
