using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public abstract class TypedScriptableEvent<T> : ScriptableObject
{
    private List<TypedScriptableEventListener<T>> _listeneres;
    private List<TypedScriptableEventListener<T>> _orderedListeners;

    private void Awake()
    {
        _listeneres = new List<TypedScriptableEventListener<T>>();
        _orderedListeners = new List<TypedScriptableEventListener<T>>();
    }

    public virtual void Register(TypedScriptableEventListener<T> listener)
    {
        if (!_listeneres.Contains(listener))
        {
            _listeneres.Add(listener);
            _orderedListeners = _listeneres.OrderByDescending(x => x.Priority).ToList();
        }
    }

    public virtual void UnRegister(TypedScriptableEventListener<T> listener)
    {
        if (_listeneres.Contains(listener))
        {
            _listeneres.Remove(listener);
            _orderedListeners = _listeneres.OrderByDescending(x => x.Priority).ToList();
        }
    }

    public virtual void Invoke(T value)
    {
        foreach (var listener in _orderedListeners)
        {
            listener?.Invoke(value);
        }
    }
}
