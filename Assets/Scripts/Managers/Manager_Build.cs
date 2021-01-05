using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_Build : MonoBehaviour
{
    public List<Interactable> Buildables;

    [Header("Events")]
    public InteractableListEvent OnShowBuildables;
    public ScriptableEvent OnRequestHover;
    public ScriptableEvent OnPlaceBuildable;

    private Interactable _selectedBuildable;

    private bool _isBuildling;

    private void Update()
    {
        if (_isBuildling)
            OnRequestHover?.Invoke();
    }

    public void ShowBuildables()
    {
        OnShowBuildables?.Invoke(Buildables);
    }

    public void SelectBuildable(Interactable buildable)
    {
        if (_isBuildling)
            return;

        _selectedBuildable = Instantiate(buildable);
        _selectedBuildable.DisableInteraction();
        _isBuildling = true;
    }

    public void HoverSelectedBuild(Vector3 position)
    {
        if (!_isBuildling)
            return;

        _selectedBuildable.transform.position = position;
    }

    public void PlaceBuildable()
    {
        if (!_isBuildling)
            return;

        OnPlaceBuildable?.Invoke();
        _selectedBuildable.EnableInteraction();
        _selectedBuildable = null;
        _isBuildling = false;
    }

    public void CancelBuildable()
    {
        if (!_isBuildling)
            return;

        Destroy(_selectedBuildable.gameObject);
        _selectedBuildable = null;
        _isBuildling = false;
    }
}
