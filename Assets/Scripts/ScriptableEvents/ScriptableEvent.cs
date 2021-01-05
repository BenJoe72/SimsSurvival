using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableEvents/Event")]
public class ScriptableEvent : ScriptableObject
{
    private List<ScriptableEventListener> _listeneres;

    private List<ScriptableEventListener> _orderedListeners;

    private void Awake()
    {
        _listeneres = new List<ScriptableEventListener>();
    }

    public void Register(ScriptableEventListener listener)
    {
        if (!_listeneres.Contains(listener))
        {
            _listeneres.Add(listener);
            _orderedListeners = _listeneres.OrderByDescending(x => x.Priority).ToList();
        }
    }

    public void UnRegister(ScriptableEventListener listener)
    {
        if (_listeneres.Contains(listener))
        {
            _listeneres.Remove(listener);
            _orderedListeners = _listeneres.OrderByDescending(x => x.Priority).ToList();
        }
    }

    public void Invoke()
    {
        foreach (var listener in _orderedListeners)
        {
            listener?.Invoke();
        }
    }
}
