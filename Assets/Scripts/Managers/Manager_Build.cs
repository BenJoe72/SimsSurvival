using UnityEngine;
using System.Collections;

public class Manager_Build : MonoBehaviour
{
    public Transform HouseContainer;
    public int MinBuildingBlockHeight;
    public int MaxBuildingBlockHeight;

    [Header("Events")]
    public ScriptableEvent RebuildNavmeshEvent;

    private BuildingBlock _createdBuildingBlock;
    private BuildingBlock _selectedBuildingBlock;
    private float _currentBlockHeight;

    public void RotateBuildingClockwise()
    {
        if (_createdBuildingBlock != null)
            _createdBuildingBlock.RotateClockwise();
    }

    public void RotateBuildingCounterClockwise()
    {
        if (_createdBuildingBlock != null)
            _createdBuildingBlock.RotateCounterClockwise();
    }

    public void SelectBuildingBlockType(BuildingBlock selectedBlock)
    {
        CancelBuildingBlock();
        _selectedBuildingBlock = selectedBlock;
    }

    public void StartBuildingBlock(Vector3 position)
    {
        if (_selectedBuildingBlock == null)
            return;

        _createdBuildingBlock = Instantiate(_selectedBuildingBlock, HouseContainer);
        _createdBuildingBlock.RecalculatePosition(position);
        _createdBuildingBlock.RecalculateSize(position);
        _currentBlockHeight = _createdBuildingBlock.transform.position.y;
    }

    public void DragBuildingBlock(Vector3 position)
    {
        if (_createdBuildingBlock != null)
        {
            _createdBuildingBlock.RecalculateSize(position);
        }
    }

    public void PlaceBuildingBlock(Vector3 position)
    {
        if (_createdBuildingBlock != null && !_createdBuildingBlock.IsValidPlacement)
            CancelBuildingBlock();

        _createdBuildingBlock = null;
        RebuildNavmeshEvent?.Invoke();
    }

    public void RaiseBuildingBlock()
    {
        if (_createdBuildingBlock != null && _currentBlockHeight < MaxBuildingBlockHeight)
        {
            _currentBlockHeight++;
            _createdBuildingBlock.RecalculateHeight(_currentBlockHeight);
        }
    }

    public void LowerBuildingBlock()
    {
        if (_createdBuildingBlock != null && _currentBlockHeight > MinBuildingBlockHeight)
        {
            _currentBlockHeight--;
            _createdBuildingBlock.RecalculateHeight(_currentBlockHeight);
        }
    }

    public void CancelBuildingBlock()
    {
        if (_createdBuildingBlock != null)
            RemoveBlock(_createdBuildingBlock);
    }

    public void RemoveBlock(Interactable interactable)
    {
        if (_selectedBuildingBlock != null)
            return;

        BuildingBlock block = interactable.GetComponent<BuildingBlock>();
        if (block != null)
        {
            RemoveBlock(block);
        }
    }

    private void RemoveBlock(BuildingBlock block)
    {
        block.Destroy();
        RebuildNavmeshEvent?.Invoke();
    }
}
