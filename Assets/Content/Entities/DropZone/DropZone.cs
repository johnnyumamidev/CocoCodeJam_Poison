using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DropZone : MonoBehaviour
{
    // create a container holding any dropped gameObjects
    public List<Item> items = new List<Item>();

    public List<ItemType> itemsRequiredToBrew = new();

    public void AddItem(Item _item)
    {
        items.Add(_item);
        _item.transform.SetParent(transform);
    }

    public void BrewPotion()
    {
        //check that the lists are the same length
        bool listCountsAreEqual = items.Count == itemsRequiredToBrew.Count;
        
        int numberOfRequiredItemsFound = 0;

        //double for loop through lists
        for(int i = 0; i < itemsRequiredToBrew.Count; i++)
        {
            for(int j = 0; j < items.Count; j++)
            {
                ItemType itemType = items[j].GetItemType();
                if(itemType == itemsRequiredToBrew[i])
                {
                    numberOfRequiredItemsFound++;
                    break;
                }
            }
        }
        
        //
        bool requiredItemsFound = numberOfRequiredItemsFound == itemsRequiredToBrew.Count;
        
        bool success = listCountsAreEqual && requiredItemsFound;

        if(success)
            Events.OnSuccessfulBrew();
        else 
            Events.OnFailedBrew();
    }

    public void ClearItems()
    {
        foreach(Item item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }
}
