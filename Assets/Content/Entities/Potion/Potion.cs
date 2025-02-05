using UnityEngine;

public class Potion : MonoBehaviour
{
    bool isBeingDragged = false;
    [SerializeField] PotionType potionType;

    [SerializeField] SpriteRenderer potionRenderer;
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePoint2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(isBeingDragged)
            transform.position = mousePoint2D;
    }

    void OnMouseDown()
    {
        isBeingDragged = true;
    }
    void OnMouseUp()
    {
        isBeingDragged = false;

        //Check for Customer on drop
        Collider2D col = Physics2D.OverlapCircle(transform.position, 1f);
        if(col && col.TryGetComponent(out Customer customer))
        {
            customer.RecievePotion(this);
            Destroy(gameObject);
        }
    }

    public void SetPotionType(PotionSO _potionData)
    {
        potionType = _potionData.potionType;
        
        //Assign name
        name = potionType.ToString() + " Potion";
        
        //Change Sprite Color
        potionRenderer.color = _potionData.potionColor;
    }
    public PotionType GetPotionType()
    {
        return potionType;
    }
}
