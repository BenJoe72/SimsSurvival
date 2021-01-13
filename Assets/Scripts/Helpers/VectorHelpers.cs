using UnityEngine;
using System.Collections;

public static class VectorHelpers
{
    public static Vector3Int RoundToVectorInt(this Vector3 vector)
    {
        return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
}
