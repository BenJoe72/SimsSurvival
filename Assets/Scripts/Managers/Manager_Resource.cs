using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager_Resource : MonoBehaviour
{
    public Resource_Data[] data;

    public bool HasResources(BuildPrice[] prices)
    {
        bool result = true;

        if (prices != null)
        {
            foreach (var price in prices)
            {
                result &= data.Any(x => x.definition == price.Type && x.currentValue >= price.Value);
            }
        }

        return result;
    }

    public void PayPrice(BuildPrice[] prices)
    {
        foreach (var price in prices)
        {
            var type = data.FirstOrDefault(x => x.definition == price.Type);
            if (type != null)
                type.Add(-price.Value);
            else
                Debug.LogError("You can not pay for what you have placed!");
        }
    }
}
