using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemType itemType;
    [SerializeField] float dropCheckRadius = 0.5f;
    void OnEnable()
    {
        Events.OnItemDropped += CheckForDropZone; 
    }
    void OnDisable()
    {
        Events.OnItemDropped -= CheckForDropZone;
    }

    void CheckForDropZone(GameObject objDropped)
    {
        if(objDropped != gameObject)
            return;

        Collider2D collider = Physics2D.OverlapCircle(transform.position, dropCheckRadius);
        if(collider && collider.TryGetComponent(out DropZone dropZone))
        {
            dropZone.AddItem(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ItemType GetItemType()
    {
        return itemType;
    }
}


public enum ItemType 
{
    A, B, C, D, E
}
