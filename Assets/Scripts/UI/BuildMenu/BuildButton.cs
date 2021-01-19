using UnityEngine;
using System.Collections;

public class BuildButton : MonoBehaviour
{
    public BuildingBlock Block;

    [Header("Event")]
    public BuildingBlockEvent ClickEvent;

    public void Select()
    {
        ClickEvent?.Invoke(Block);
    }
}
