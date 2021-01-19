using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FurnishMenu_Generator : MonoBehaviour
{
    public FurnishMenu_Button Prefab;
    public Transform Container;

    private List<FurnishMenu_Button> _buttons;

    private void Awake()
    {
        _buttons = new List<FurnishMenu_Button>();
    }

    public void Generate(List<Interactable> interactables)
    {
        Clear();

        foreach (var interactable in interactables)
        {
            FurnishMenu_Button newBtn = Instantiate(Prefab, Container);
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
