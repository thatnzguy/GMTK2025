using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour, IInteractable {
    
    [SerializeField] private Outline _outline;

    [SerializeField] private UnityEvent onInteract;

    private void Awake()
    {
        FocusOff();
    }

    public bool CanFocus(Interactor interactor)
    {
        return true;
    }

    public virtual void FocusOn()
    {
        _outline.enabled = true;
    }

    public void FocusOff()
    {
        _outline.enabled = false;
    }

    public virtual bool Interact(Interactor interactor)
    {
        onInteract?.Invoke();
        return false;
    }
}