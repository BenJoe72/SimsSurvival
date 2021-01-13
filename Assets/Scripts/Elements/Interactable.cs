using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;

public class Interactable : MonoBehaviour
{
    public List<BaseAction> Intearctions;
    public Transform InteractionPoint;
    public Sprite Icon;
    public BuildPrice[] BuildPrice;
    public float OccupyArea = 1f;
    public bool Generated = false;
    
    public bool CanBuild { get; private set; }

    public UnityEvent InteractedResponse;

    private Collider _collider;
    private Renderer[] _renderers;
    private Material[][] _materials;

    public void EvaluateBuildable(Manager_Resource resource)
    {
        CanBuild = resource.HasResources(BuildPrice);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderers = GetComponentsInChildren<Renderer>(true);
        _materials = new Material[_renderers.Length][];
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

    public void SetMaterial(Material buildMaterial)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _materials[i] = _renderers[i].sharedMaterials;
            Material[] mats = _renderers[i].materials;

            for (int j = 0; j < _renderers[i].materials.Length; j++)
            {
                mats[j] = buildMaterial;
            }

            _renderers[i].materials = mats;
        }
    }

    public void ResetMaterial()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].materials = _materials[i];
        }
    }

    public bool CheckOverLaps()
    {
        var overlaps = Physics.SphereCastAll(new Ray(transform.position, Vector3.up), OccupyArea, OccupyArea, (int)Mathf.Pow(2, gameObject.layer));
        bool result = overlaps.Any(x => x.collider.gameObject != gameObject);

        foreach (var renderer in _renderers)
        {
            Material[] mats = renderer.materials;

            foreach (var mat in mats)
            {
                mat.SetInt("FreeToPlace", result ? 0 : 1);
            }

            renderer.materials = mats;
        }

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, OccupyArea);
    }
}
