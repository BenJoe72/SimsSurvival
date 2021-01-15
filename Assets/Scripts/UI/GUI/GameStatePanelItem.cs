using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStatePanelItem : MonoBehaviour
{
    public GameState State;
    public Image Icon;

    private void Start()
    {
        Icon.sprite = State.Icon;
    }
}
