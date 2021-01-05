using UnityEngine;
using System.Collections;

public class BuildMenu_Button : MonoBehaviour
{
    public Interactable Element;

    [Header("Events")]
    public InteractableEvent OnSelectElement;

    public void OnClick()
    {
        OnSelectElement?.Invoke(Element);
    }
}
