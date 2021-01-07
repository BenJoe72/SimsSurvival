using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resource_Display : MonoBehaviour
{
    public Resource_DataDefinition _Data;
    public Image _Icon;
    public TextMeshProUGUI _Text;

    private string _textTemplate;

    private void Awake()
    {
        _textTemplate = _Text.text;
        _Icon.sprite = _Data.icon;
    }

    public void UpdateValue(float value)
    {
        _Text.text = string.Format(_textTemplate, value, _Data.maxValue);
    }
}
