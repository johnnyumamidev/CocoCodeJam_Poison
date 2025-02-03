using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    private bool isHoldingItem;
    GameObject heldItem;
    void OnEnable()
    {
        Events.OnItemSpawned += HoldItem;
    }
    void OnDisable()
    {
        Events.OnItemSpawned -= HoldItem;
    }
    
    void HoldItem(GameObject _heldItem)
    {
        isHoldingItem = true;
        
        heldItem = _heldItem;
    }
    void DropItem()
    {
        Events.OnItemDropped(heldItem);

        isHoldingItem = false;
        heldItem = null;
    }

    public bool GetHoldingState()
    {
        return isHoldingItem;
    }

    void Update()
    {
        if(isHoldingItem)
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            heldItem.transform.position = mousePoint;

            if(Input.GetMouseButtonUp(0))
            {
                DropItem();
            }
        }
    }
}
