using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public abstract class StateBrain : MonoBehaviour
{
    public ScriptableEvent OnEnterState;
    public ScriptableEvent OnExitState;

    public virtual void EnterState()
    {
        OnEnterState?.Invoke();
    }

    public virtual void ExitState()
    {
        OnExitState?.Invoke();
    }
}
