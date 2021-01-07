using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildMenu_Button : MonoBehaviour
{
    public Interactable Element;
    public Image IconImage;
    private Button _button;
    public BuildMenu_ButtonHint Hint;

    [Header("Events")]
    public InteractableEvent OnSelectElement;

    public void Intialize(Interactable element)
    {
        _button = GetComponent<Button>();

        Element = element;
        _button.interactable = element.CanBuild;
        if (element._Icon != null) IconImage.sprite = element._Icon;

        Hint.SetPrice(element._BuildPrice);
        Hint.HideHint();
    }

    public void OnClick()
    {
        OnSelectElement?.Invoke(Element);
    }
}
