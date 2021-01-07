using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildMenu_Generator : MonoBehaviour
{
    public BuildMenu_Button Prefab;
    public Transform Container;

    private List<BuildMenu_Button> _buttons;

    private void Awake()
    {
        _buttons = new List<BuildMenu_Button>();
    }

    public void Generate(List<Interactable> interactables)
    {
        Clear();

        foreach (var interactable in interactables)
        {
            BuildMenu_Button newBtn = Instantiate(Prefab, Container);
            newBtn.Intialize(interactable);
            _buttons.Add(newBtn);
        }
    }

    private void Clear()
    {
        foreach (var buton in _buttons)
        {
            Destroy(buton.gameObject);
        }

        _buttons.Clear();
    }
}
