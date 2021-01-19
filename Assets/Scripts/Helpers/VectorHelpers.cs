using UnityEngine;
using System.Collections;

public static class VectorHelpers
{
    public static Vector3Int RoundToVectorInt(this Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }

    public static Vector3 ByAxisMultiply(this Vector3 first, Vector3 second)
    {
        return new Vector3(first.x * second.x, first.y * second.y, first.z * second.z);
    }

    public static Vector3Int SetMinimumSize(this Vector3Int vector, Vector3Int minimum)
    {
        return new Vector3Int(Mathf.Max(vector.x, minimum.x), Mathf.Max(vector.y, minimum.y), Mathf.Max(vector.z, minimum.z));
    }

    public static Vector3Int Absolute(this Vector3Int vector)
    {
        return new Vector3Int(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }
}
