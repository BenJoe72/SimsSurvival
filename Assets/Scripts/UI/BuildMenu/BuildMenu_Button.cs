using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildMenu_Button : MonoBehaviour
{
    public Interactable Element;
    public Image IconImage;

    [Header("Events")]
    public InteractableEvent OnSelectElement;

    public void OnClick()
    {
        OnSelectElement?.Invoke(Element);
    }
}
