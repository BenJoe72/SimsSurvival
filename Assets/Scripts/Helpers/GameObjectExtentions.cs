using UnityEngine;
using System.Collections;

public static class GameObjectExtentions
{
    public static bool GetIsPrefab(this GameObject a_Object)
    {
        return a_Object.scene.rootCount == 0;
    }

    public static T GetRandom<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }
}
