using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(Interactable))]
public abstract class BuildingBlock : MonoBehaviour
{
    public Renderer BuildingRenderer;

    protected Interactable _interactable;
    protected Material[] _materials;
    protected Vector3 _originalPosition;
    protected Vector3 _lastPoint;
    protected Facing _facing;

    public bool IsValidPlacement { get { return !_interactable.CheckOverLaps(); } }
    
    private void Awake()
    {
        _materials = BuildingRenderer.materials;
        _interactable = GetComponent<Interactable>();
    }

    public virtual void RotateCounterClockwise()
    {
        int facing = (int)_facing + 1;
        if (facing > 3)
            facing = 0;

        transform.Rotate(transform.up, -90f);

        RecalculateRotation((Facing)facing);
    }

    public virtual void RotateClockwise()
    {
        int facing = (int)_facing - 1;
        if (facing < 0)
            facing = 3;

        transform.Rotate(transform.up, 90f);

        RecalculateRotation((Facing)facing);
    }

    public virtual void RecalculateRotation(Facing facing)
    {
        _facing = facing;
        RecalculateSize(_lastPoint);
    }

    public virtual void RecalculatePosition(Vector3 point)
    {
        Vector3Int position = (point - transform.position).RoundToVectorInt();
        transform.position = position;
        _originalPosition = position;

        ValidatePlacement();
        _lastPoint = point;
    }

    public virtual void RecalculateHeight(float height)
    {
        Vector3Int position = new Vector3(transform.position.x, height, transform.position.z).RoundToVectorInt();
        transform.position = position;
        _originalPosition.y = position.y;

        ValidatePlacement();
    }

    public virtual Vector3Int RecalculateSize(Vector3 point)
    {
        Vector3Int size = (point - _originalPosition).RoundToVectorInt();
        Vector3 newPosition = _originalPosition + new Vector3(Mathf.Min(size.x, 0), 0, Mathf.Min(size.z, 0));
        size = size.Absolute().SetMinimumSize(Vector3Int.one);

        switch (_facing)
        {
            default:
            case Facing.NORTH:
                break;
            case Facing.WEST:
                newPosition = newPosition + Vector3.right * size.x;
                size = new Vector3Int(size.z, size.y, size.x);
                break;
            case Facing.SOUTH:
                Vector3 flatSize = size;
                flatSize.y = 0;
                newPosition = newPosition + flatSize;
                break;
            case Facing.EAST:
                newPosition = newPosition + Vector3.forward * size.z;
                size = new Vector3Int(size.z, size.y, size.x);
                break;
        }

        transform.position = newPosition;
        transform.localScale = size;

        ValidatePlacement();
        _lastPoint = point;

        return size;
    }

    protected virtual void ValidatePlacement()
    {
        bool result = _interactable.CheckOverLaps();

        foreach (var mat in _materials)
            mat.SetInt("InvalidPlace", result ? 1 : 0);
    }

    public virtual void Destroy()
    {
        foreach (var coll in GetComponentsInChildren<Collider>())
            coll.enabled = false;

        Destroy(gameObject);
    }

    public enum Facing
    {
        NORTH = 0,
        WEST = 1,
        SOUTH = 2,
        EAST = 3
    }
}
