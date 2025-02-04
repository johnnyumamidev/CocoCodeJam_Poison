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

    public PotionSO potionToBrewData;

    void OnEnable()
    {
        Events.OnCustomerEnter += SetOrderData;
    }
    void OnDisable()
    {
        Events.OnCustomerEnter -= SetOrderData;
    }

    void SetOrderData(PotionSO _potionToBrew)
    {
        potionToBrewData = _potionToBrew;
    }
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
        List<Item> itemsToCheck = new List<Item>(items);
        //check that the lists are the same length
        bool listCountsAreEqual = itemsToCheck.Count == potionToBrewData.itemsRequiredToBrew.Count;
        int numberOfRequiredItemsFound = 0;

        //double for loop through lists
        for(int i = 0; i < potionToBrewData.itemsRequiredToBrew.Count; i++)
        {
            for(int j = 0; j < itemsToCheck.Count; j++)
            {
                ItemType itemType = itemsToCheck[j].GetItemType();
                if(itemType == potionToBrewData.itemsRequiredToBrew[i])
                {
                    numberOfRequiredItemsFound++;
                    itemsToCheck.Remove(itemsToCheck[j]);
                    break;
                }
            }
        }
 
        bool requiredItemsFound = numberOfRequiredItemsFound == potionToBrewData.itemsRequiredToBrew.Count;
        bool success = listCountsAreEqual && requiredItemsFound;

        if(success)
            Events.OnSuccessfulBrew(potionToBrewData);
        else 
            Events.OnFailedBrew();

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
