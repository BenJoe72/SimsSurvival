using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildMenu_ButtonHint : MonoBehaviour
{
    public BuildMenu_ButtonHintEntry EntryPrefab;

    private bool hasPrice;

    public void SetPrice(BuildPrice[] prices)
    {
        hasPrice = prices.Length > 0;

        foreach (var price in prices)
        {
            BuildMenu_ButtonHintEntry newentry = Instantiate(EntryPrefab, transform);
            newentry.SetPrice(price);
        }
    }

    public void ShowHint()
    {
        gameObject.SetActive(hasPrice);
    }

    public void HideHint()
    {
        gameObject.SetActive(false);
    }
}
