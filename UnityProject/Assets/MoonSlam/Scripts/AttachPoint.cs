using System;
using UnityEngine;

public class AttachPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private Outline _outline;
    [SerializeField] private Color _focusColor;
    [SerializeField] private float _attachRadius = 0.5f;
    private Collider[] _overlapColliders = new Collider[10];

    private Attachable _attached;

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

    public bool CanFocus(Interactor interactor)
    {
        return interactor.HeldItem != null && _attached == null;
    }

    public void FocusOn()
    {
        //TODO IDK why this doesn't change the color
        _outline.OutlineColor = _focusColor;
    }

    public void FocusOff()
    {
        _outline.OutlineColor = Color.white;
    }

    public bool Interact(Interactor interactor)
    {
        if (interactor.HeldItem != null)
        {
            Attachable attachable = interactor.HeldItem.GetComponent<Attachable>();
            if (attachable != null)
            {
                attachable.Attach(this);
                _attached = attachable;
                _outline.enabled = false;
                FocusOff();
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _attachRadius);
    }
}
