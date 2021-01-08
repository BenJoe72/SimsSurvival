using UnityEngine;
using System.Collections;

public class TypedScriptableEventTrigger<T> : MonoBehaviour
{
    public TypedScriptableEvent<T> Event;

    public void TriggerEvent(T value)
    {
        Event?.Invoke(value);
    }
}
