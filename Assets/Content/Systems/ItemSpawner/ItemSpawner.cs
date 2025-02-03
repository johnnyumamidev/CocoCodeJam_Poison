using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    int itemIndex = 0;
    
    void OnMouseDown()
    {
        //Spawn Item
        GameObject spawnedItem = Instantiate(itemPrefab, transform);
        
        itemIndex++;
        spawnedItem.name = "Item " + itemIndex.ToString(); 
        
        Events.OnItemSpawned(spawnedItem);
    }
}
