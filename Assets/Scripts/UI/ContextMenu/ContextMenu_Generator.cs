using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenu_Generator : MonoBehaviour
{
    public ContextMenu_Button _ButtonPrefab;
    public Transform _ContextMenu;
    public Transform _ClearButton;

    private List<ContextMenu_Button> _buttons;

    private bool _enabled;
    private bool _generated;

    private void Start()
    {
        _buttons = new List<ContextMenu_Button>();

        Disable();
    }

    public void Generate(Interaction interaction)
    {
        if (_generated)
            return;

        Clear();

        // Select only the interactions that can actually be performed
        var performableInteracitons = interaction.interactable._Intearctions.Where(x => x.CanPerform(interaction)).ToList();

        if (performableInteracitons.Count > 0)
        {
            float angleOffset = 360 / performableInteracitons.Count;
            float currentAngle = 0;

            for (int i = 0; i < performableInteracitons.Count; i++)
            {
                BaseAction action = performableInteracitons[i];

                Vector3 currentPos = (Vector3.up * 75f);
                currentPos = Quaternion.AngleAxis(currentAngle, Vector3.forward) * currentPos;
                currentPos += _ContextMenu.position;
                currentAngle += angleOffset;

                ContextMenu_Button newbutton = Instantiate(_ButtonPrefab, currentPos, Quaternion.identity, _ContextMenu);
                _buttons.Add(newbutton);

                newbutton.OnGenerate(this);
                newbutton.interaction = new Interaction(interaction.character, interaction.interactable, action, interaction.position);
            }
        }

        _generated = true;
    }

    private void Clear()
    {
        foreach (var button in _buttons)
            Destroy(button.gameObject);

        _buttons.Clear();
    }

    public void Enable(Vector2 position)
    {
        if (_enabled)
            return;

        _enabled = true;
        _ContextMenu.gameObject.SetActive(true);
        _ClearButton.gameObject.SetActive(true);
        transform.position = position;
    }

    public void Disable()
    {
        _enabled = false;
        _generated = false;
        _ContextMenu.gameObject.SetActive(false);
        _ClearButton.gameObject.SetActive(false);
    }
}
