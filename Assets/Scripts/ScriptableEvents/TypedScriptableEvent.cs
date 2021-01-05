using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class TypedScriptableEvent<T> : ScriptableObject
{
    private List<TypedScriptableEventListener<T>> _listeneres;

    private void Awake()
    {
        _listeneres = new List<TypedScriptableEventListener<T>>();
    }

    public virtual void Register(TypedScriptableEventListener<T> listener)
    {
        if (!_listeneres.Contains(listener))
        {
            _listeneres.Add(listener);
        }
    }

    public virtual void UnRegister(TypedScriptableEventListener<T> listener)
    {
        if (_listeneres.Contains(listener))
        {
            _listeneres.Remove(listener);
        }
    }

    public virtual void Invoke(T value)
    {
        foreach (var listener in _listeneres)
        {
            listener?.Invoke(value);
        }
    }
}
