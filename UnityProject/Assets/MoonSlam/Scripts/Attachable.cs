using UnityEngine;

public class Attachable : Pickupable
{
    private bool _attached;

    private AudioSource jumpSource;

    public void Attach(AttachPoint attachPoint)
    {
        if (_isHeld)
        {
            _holdingInteractor?.ReleaseHeldItem();
        }
        
        _rigidbody.isKinematic = true;
        transform.position = attachPoint.transform.position;
        transform.rotation = attachPoint.transform.rotation;
        
        _attached = true;
        jumpSource.Play();
    }

    public override bool Interact(Interactor interactor)
    {
        return !_attached;
    }
}
