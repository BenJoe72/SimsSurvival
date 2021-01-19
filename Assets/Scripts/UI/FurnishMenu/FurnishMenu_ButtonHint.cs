using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FurnishMenu_ButtonHint : MonoBehaviour
{
    public FurnishMenu_ButtonHintEntry EntryPrefab;

    private bool hasPrice;

    public void SetPrice(BuildPrice[] prices)
    {
        hasPrice = prices.Length > 0;

        foreach (var price in prices)
        {
            FurnishMenu_ButtonHintEntry newentry = Instantiate(EntryPrefab, transform);
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
