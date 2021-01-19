using UnityEngine;
using System.Collections;

public class Ramp : BuildingBlock
{
    public Vector3 Offset;

    public override void RecalculatePosition(Vector3 point)
    {
        base.RecalculatePosition(point);
        transform.position += Offset;
        _originalPosition = transform.position;

        ValidatePlacement();
    }

    public override void RecalculateHeight(float height)
    {
        base.RecalculateHeight(height);
        transform.position += Offset;
        _originalPosition.y = transform.position.y;

        ValidatePlacement();
    }

    public override Vector3Int RecalculateSize(Vector3 point)
    {
        Vector3Int size = base.RecalculateSize(point);
        size.y = size.z;
        transform.localScale = size;
        ValidatePlacement();

        _materials[0].SetVector("RepeatSize", new Vector4(size.x, size.z, 0, 0));

        return size;
    }
}
