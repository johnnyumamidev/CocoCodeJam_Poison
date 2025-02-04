using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DropZone : MonoBehaviour
{
    // create a container holding any dropped gameObjects
    public List<Item> items = new List<Item>();

//TODO REFACTOR
    //change system so that there's a list of possible recipes always available rather than only the correct recipe
// ========================================================================

    //list of possible potions that can be made
    [SerializeField] List<PotionSO> potionRecipes = new List<PotionSO>();

// ========================================================================

    //Add ingredients to list when dropped
    public void AddItem(Item _item)
    {
        items.Add(_item);
        _item.transform.SetParent(transform);
    }

//TODO REFACTOR BREW FUNCTION
//  loop through list of potion recipes
//  if ingredients match one of the recipes, make that potion and break loop
// ========================================================================
    public void BrewPotion()
    {
        //create a new list that contains the dropped items
        int failedBrewCount = 0;

        foreach(PotionSO potionRecipe in potionRecipes)
        {
            List<Item> itemsToCheck = new List<Item>(items);
            //check that the lists are the same length
            bool listCountsAreEqual = itemsToCheck.Count == potionRecipe.itemsRequiredToBrew.Count;
            int numberOfRequiredItemsFound = 0;
            Debug.Log("num ingredients found: " + itemsToCheck.Count);
            Debug.Log(potionRecipe.name + " ingredients count: " + potionRecipe.itemsRequiredToBrew.Count);
            //double for loop through lists
            for(int i = 0; i < potionRecipe.itemsRequiredToBrew.Count; i++)
            {
                for(int j = 0; j < itemsToCheck.Count; j++)
                {
                    ItemType itemType = itemsToCheck[j].GetItemType();
                    if(itemType == potionRecipe.itemsRequiredToBrew[i])
                    {
                        numberOfRequiredItemsFound++;
                        itemsToCheck.Remove(itemsToCheck[j]);
                        break;
                    }
                }
            }
    
            bool requiredItemsFound = numberOfRequiredItemsFound == potionRecipe.itemsRequiredToBrew.Count;
            bool success = listCountsAreEqual && requiredItemsFound;

            if(success)
            {
                Events.OnSuccessfulBrew(potionRecipe);
                break;
            }
            else
            {
                Debug.Log(potionRecipe.name + " brew FAIL due to:");
                Debug.Log("num of ingredients equal? " + listCountsAreEqual);
                Debug.Log("required ingreients found? " + requiredItemsFound);
                failedBrewCount++;
                continue;
            }
        }

        //if ingredients list does not match ANY of the available recipes, failed brew
        if(failedBrewCount >= potionRecipes.Count)
        {
            Events.OnFailedBrew();
        }

        ClearItems();
    }
// ========================================================================

    public void ClearItems()
    {
        foreach(Item item in items)
        {
            Destroy(item.gameObject);
        }
        items.Clear();
    }
}
