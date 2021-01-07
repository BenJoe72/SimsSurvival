using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public List<BaseAction> _Intearctions;
    public Transform _InteractionPoint;
    public Sprite _Icon;
    public BuildPrice[] _BuildPrice;

    public bool CanBuild { get; private set; }

    public UnityEvent InteractedResponse;

    private Collider _collider;

    public void EvaluateBuildable(Manager_Resource resource)
    {
        CanBuild = resource.HasResources(_BuildPrice);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    public void Interact()
    {
        InteractedResponse?.Invoke();
    }

    public void EnableInteraction()
    {
        if (_collider != null)
            _collider.enabled = true;
    }

    public void DisableInteraction()
    {
        if (_collider != null)
            _collider.enabled = false;
    }
}
