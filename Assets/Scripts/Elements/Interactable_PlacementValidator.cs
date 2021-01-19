using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Interactable_PlacementValidator : MonoBehaviour
{
    public bool IsValidPlacement { get { return _overlaps.Count <= 0; } }

    private List<Interactable_PlacementValidator> _overlaps;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _overlaps = new List<Interactable_PlacementValidator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable_PlacementValidator interactable = other.GetComponent<Interactable_PlacementValidator>();

        if (interactable != null && !_overlaps.Contains(interactable))
            _overlaps.Add(interactable);
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable_PlacementValidator interactable = other.GetComponent<Interactable_PlacementValidator>();

        if (interactable != null && _overlaps.Contains(interactable))
            _overlaps.Remove(interactable);
    }
}
