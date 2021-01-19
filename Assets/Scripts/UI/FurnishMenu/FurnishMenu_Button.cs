using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FurnishMenu_Button : MonoBehaviour
{
    public Interactable Element;
    public Image IconImage;
    private Button _button;
    public FurnishMenu_ButtonHint Hint;

    [Header("Events")]
    public InteractableEvent OnSelectElement;

    public void Intialize(Interactable element)
    {
        _button = GetComponent<Button>();

        Element = element;
        _button.interactable = element.CanBuild;
        if (element.Icon != null) IconImage.sprite = element.Icon;

        Hint.SetPrice(element.BuildPrice);
        Hint.HideHint();
    }

    public void OnClick()
    {
        OnSelectElement?.Invoke(Element);
    }
}
