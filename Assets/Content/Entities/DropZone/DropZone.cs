using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DropZone : MonoBehaviour
{
    // create a container holding any dropped gameObjects
    public List<Item> items = new List<Item>();

    //SFX SOURCE
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioClip splashClip;

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

        //Play splashSFX
        SFX.PlayOneShot(splashClip);
    }

//TODO REFACTOR BREW FUNCTION
//  loop through list of potion recipes
//  if ingredients match one of the recipes, make that potion and break loop
// ========================================================================
    public void BrewPotion()
    {
        int failedBrewCount = 0;

        foreach(PotionSO potionRecipe in potionRecipes)
        {
            //create a new list that contains the dropped items
            List<Item> itemsToCheck = new List<Item>(items);
            
            //check that the lists are the same length
            bool listCountsAreEqual = itemsToCheck.Count == potionRecipe.itemsRequiredToBrew.Count;
            
            //intialise int checking for correct ingredients
            int numberOfRequiredItemsFound = 0;

            //double for loop
            //  for every required ingredient, go through list of ingredients in POT 
            for(int i = 0; i < potionRecipe.itemsRequiredToBrew.Count; i++)
            {
                for(int j = 0; j < itemsToCheck.Count; j++)
                {
                    // if correct ingredient is found, end loop and remove from list of ingredients to check
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
                failedBrewCount++;
                continue;
            }
        }

        //if ingredients in POT does not match ANY of the available recipes, brew failed
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
