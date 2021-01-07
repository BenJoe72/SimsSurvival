using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "DataDefinition", menuName = "Character/DataDefinition")]
public class Resource_DataDefinition : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public float maxValue = 1f;
    public float minValue = 0f;
    public float startingValue;
}
