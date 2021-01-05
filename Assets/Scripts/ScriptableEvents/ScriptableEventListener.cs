using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    public ScriptableEvent _Event;
    public int Priority;
    public UnityEvent _Activate;

    private void OnEnable()
    {
        _Event.Register(this);
    }

    private void OnDisable()
    {
        _Event.UnRegister(this);
    }

    public void Invoke()
    {
        _Activate?.Invoke();
    }
}
