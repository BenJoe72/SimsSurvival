using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class TypedScriptableEventListener<T> : MonoBehaviour
{
    public TypedScriptableEvent<T> _Event;

    public UnityEvent<T> _Activate;

    private void OnEnable()
    {
        _Event.Register(this);
    }

    private void OnDisable()
    {
        _Event.UnRegister(this);
    }

    public virtual void Invoke(T value)
    {
        _Activate?.Invoke(value);
    }
}
