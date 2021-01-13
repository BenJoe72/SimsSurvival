using UnityEngine;
using System.Collections;

public class HouseManager : MonoBehaviour
{
    public FoundationBuildScript FoundationPrefab;
    public Transform HouseContainer;
    public int MinFoundationHeight;
    public int MaxFoundationHeight;

    private FoundationBuildScript _createdFoundation;
    private float _currentFoundationHeight;

    public void StartFoundation(Vector3 position)
    {
        _createdFoundation = Instantiate(FoundationPrefab, HouseContainer);
        _createdFoundation.RecalculatePosition(position);
        _currentFoundationHeight = _createdFoundation.transform.position.y;
    }

    public void DragFoundation(Vector3 position)
    {
        _createdFoundation.RecalculateSize(position);
    }

    public void PlaceFoundation(Vector3 position)
    {
        _createdFoundation = null;
    }

    public void RaiseFoundation()
    {
        if (_currentFoundationHeight < MaxFoundationHeight)
        {
            _currentFoundationHeight++;
            _createdFoundation.RecalculateHeight(_currentFoundationHeight);
        }
    }

    public void LowerFoundation()
    {
        if (_currentFoundationHeight > MinFoundationHeight)
        {
            _currentFoundationHeight--;
            _createdFoundation.RecalculateHeight(_currentFoundationHeight);
        }
    }
}
