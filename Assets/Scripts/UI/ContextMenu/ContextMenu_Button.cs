using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Button))]
public class ContextMenu_Button : MonoBehaviour
{
    public Interaction interaction;

    private Button _button;
    private TextMeshProUGUI _buttonText;
    private ContextMenu_Generator _contextMenu;

    private void Start()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();

        _buttonText.text = interaction.action.Name;
    }

    public void OnGenerate(ContextMenu_Generator contextMenu)
    {
        _contextMenu = contextMenu;
    }

    public void OnClick()
    {
        interaction.Activate();
        _contextMenu.Disable();
    }
}
