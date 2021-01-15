using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatePanel : MonoBehaviour
{
    public float SlideTime = 1f;
    public RectTransform Slider;
    public Transform ItemContainer;
    public GameStatePanelItem[] Items;
    public Image CurrentItemImage;

    private void Start()
    {
        Close();
    }

    public void Open()
    {
        Slider.DOAnchorPos(Vector2.zero, SlideTime);
    }

    public void Close()
    {
        Slider.DOAnchorPos(Vector2.right * Slider.rect.width, SlideTime);
    }

    public void SelectItem(GameState state)
    {
        CurrentItemImage.sprite = state.Icon;

        foreach (var item in Items)
            item.gameObject.SetActive(item.State != state);
    }
}
