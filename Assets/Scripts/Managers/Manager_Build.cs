﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Manager_Build : MonoBehaviour
{
    public List<Interactable> Buildables;
    public Manager_Resource ResourceManager;

    [Header("Events")]
    public InteractableListEvent OnShowBuildables;
    public ScriptableEvent OnRequestHover;
    public ScriptableEvent OnPlaceBuildable;

    private Interactable _selectedBuildable;

    private bool _isCreating;
    private bool _isBuildling;
    private bool _isRotating;
    private bool _placedBuilding; // TODO: this is an ugly solution, do something with it
    private bool _ignoreSetRotation; // TODO: this is an ugly solution, do something with it

    private Vector3 _previousPositon;
    private Quaternion _previousRotation;

    private void Update()
    {
        if (_isBuildling)
            OnRequestHover?.Invoke();

        _placedBuilding = false;
    }

    public void ShowBuildables()
    {
        foreach (var bdbl in Buildables)
            bdbl.EvaluateBuildable(ResourceManager);

        OnShowBuildables?.Invoke(Buildables);
    }

    public void SelectBuildable(Interactable buildable)
    {
        if (_isBuildling || _placedBuilding)
            return;

        _isCreating = buildable.gameObject.GetIsPrefab();
        if (_isCreating)
            _selectedBuildable = Instantiate(buildable, Vector3.down * 100f, Quaternion.identity);
        else
        {
            _selectedBuildable = buildable;
            _previousPositon = buildable.transform.position;
            _previousRotation = buildable.transform.rotation;
            _ignoreSetRotation = true;
        }

        _selectedBuildable.DisableInteraction();
        _isBuildling = true;
    }

    public void HoverSelectedBuild(Vector3 position)
    {
        if (!_isBuildling)
            return;

        if (!_isRotating)
            _selectedBuildable.transform.position = position;
        else
            _selectedBuildable.transform.LookAt(position);
    }

    public void PlaceBuildable()
    {
        if (!_isBuildling)
            return;

        ResourceManager.PayPrice(_selectedBuildable._BuildPrice);
        OnPlaceBuildable?.Invoke();
        _isRotating = true;
    }

    public void SetRotation()
    {
        if (_selectedBuildable == null || _ignoreSetRotation || !_isRotating)
        {
            _ignoreSetRotation = false;
            return;
        }

        _selectedBuildable.EnableInteraction();
        _selectedBuildable = null;
        _isBuildling = false;
        _placedBuilding = true;
        _isRotating = false;
        ShowBuildables();
    }

    public void CancelBuildable()
    {
        if (!_isBuildling)
            return;

        if (_isCreating)
        {
            if (_selectedBuildable != null)
                Destroy(_selectedBuildable.gameObject);
        }
        else if (_selectedBuildable != null)
        {
            _selectedBuildable.transform.position = _previousPositon;
            _selectedBuildable.transform.rotation = _previousRotation;
            _selectedBuildable.EnableInteraction();
        }
        
        _selectedBuildable = null;
        _isBuildling = false;
        _isRotating = false;
    }

    public void DeleteBuildable()
    {
        if (_selectedBuildable != null)
            Destroy(_selectedBuildable.gameObject);

        CancelBuildable();
    }
}
