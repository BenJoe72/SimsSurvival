using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableEvents/Event")]
public class ScriptableEvent : ScriptableObject
{
    private List<ScriptableEventListener> _listeneres;

    private void Awake()
    {
        _listeneres = new List<ScriptableEventListener>();
    }

    public void Register(ScriptableEventListener listener)
    {
        if (!_listeneres.Contains(listener))
        {
            _listeneres.Add(listener);
        }
    }

    public void UnRegister(ScriptableEventListener listener)
    {
        if (_listeneres.Contains(listener))
        {
            _listeneres.Remove(listener);
        }
    }

    public void Invoke()
    {
        foreach (var listener in _listeneres)
        {
            listener?.Invoke();
        }
    }
}
