using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu_ButtonHintEntry : MonoBehaviour
{
    public Image Icon;
    public TextMeshProUGUI Text;

    public void SetPrice(BuildPrice price)
    {
        Icon.sprite = price.Type.icon;
        Text.text = price.Value.ToString();
    }
}
