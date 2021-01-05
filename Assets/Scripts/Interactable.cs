using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<BaseAction> _Intearctions;
    public Transform _InteractionPoint;

    public UnityEvent InteractedResponse;

    public void Interact()
    {
        InteractedResponse?.Invoke();
    }
}
