using UnityEngine;
using System.Collections;

public class Wall : Ramp
{
    public int height;


    public override void RotateCounterClockwise()
    {
        int facing = (int)_facing + 1;
        if (facing > 1)
        {
            facing = 0;
            transform.Rotate(transform.up, 90f);
        }
        else
        {
            transform.Rotate(transform.up, -90f);
        }

        RecalculateRotation((Facing)facing);
    }

    public override void RotateClockwise()
    {
        int facing = (int)_facing - 1;
        if (facing < 0)
        { 
            facing = 1;
            transform.Rotate(transform.up, -90f);
        }
        else
        {
            transform.Rotate(transform.up, 90f);
        }

        RecalculateRotation((Facing)facing);
    }

    public override Vector3Int RecalculateSize(Vector3 point)
    {
        Vector3Int size = base.RecalculateSize(point);
        size.y = height;
        size.z = 1;
        transform.localScale = size;

        Vector3 newPos = transform.position;
        if (_facing == Facing.NORTH)
            newPos.z = _originalPosition.z;
        else
            newPos.x = _originalPosition.x;
        transform.position = newPos;

        ValidatePlacement();

        _materials[0].SetVector("RepeatSize", new Vector4(size.x, height, 0, 0));

        return size;
    }
}
