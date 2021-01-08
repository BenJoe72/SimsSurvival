using UnityEngine;
using System.Collections;

public class ScriptableEventTrigger : MonoBehaviour
{
    public ScriptableEvent Event;

    public void TriggerEvent()
    {
        Event?.Invoke();
    }
}
