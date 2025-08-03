using UnityEngine;

[SelectionBase]
public class Attachable : Pickupable
{
    private bool _attached;

    public void Attach(AttachPoint attachPoint)
    {
        if (_isHeld)
        {
            _holdingInteractor?.ReleaseHeldItem();
        }
        Destroy(_rigidbody); // Game jam hack, we're not detaching anything
        // _rigidbody.isKinematic = true;
        transform.SetParent(attachPoint.transform);
        transform.position = attachPoint.transform.position;
        transform.rotation = attachPoint.transform.rotation;
        
        _attached = true;
    }
}
