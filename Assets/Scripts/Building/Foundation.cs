using UnityEngine;
using System.Collections;

public class Foundation : BuildingBlock
{
    public override Vector3Int RecalculateSize(Vector3 point)
    {
        Vector3Int size = base.RecalculateSize(point);
        size.y = 1;
        transform.localScale = size;
        ValidatePlacement();

        _materials[0].SetVector("RepeatSize", new Vector4(size.x, size.z, 0, 0));
        _materials[1].SetVector("RepeatSize", new Vector4(size.z, 1, 0, 0));
        _materials[2].SetVector("RepeatSize", new Vector4(size.x, 1, 0, 0));
        _materials[3].SetVector("RepeatSize", new Vector4(size.z, 1, 0, 0));
        _materials[4].SetVector("RepeatSize", new Vector4(size.x, 1, 0, 0));

        return size;
    }
}
