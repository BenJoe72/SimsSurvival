using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Resource_StatDisplay : MonoBehaviour
{
    public Resource_DataDefinition _Data;
    public Image _Icon;
    public Slider _Slider;

    private string _textTemplate;

    private void Awake()
    {
        _Icon.sprite = _Data.icon;
    }

    public void UpdateValue(float value)
    {
        _Slider.value = Mathf.InverseLerp(_Data.minValue, _Data.maxValue, value);
    }
}
