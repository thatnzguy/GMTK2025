using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour, IInteractable {
    
    [SerializeField] protected Outline _outline;

    [SerializeField] private UnityEvent onInteract;

    private void Awake()
    {
        FocusOff();
    }

    public virtual bool CanFocus(Interactor interactor)
    {
        return true;
    }

    public virtual void FocusOn()
    {
        _outline.enabled = true;
    }

    public virtual void FocusOff()
    {
        _outline.enabled = false;
    }

    public virtual void Interact(Interactor interactor)
    {
        onInteract?.Invoke();
    }
}