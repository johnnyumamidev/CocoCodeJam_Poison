using UnityEngine;

public class Customer : MonoBehaviour
{
    public PotionSO potionRequest;
    void Start()
    {
        Events.OnCustomerEnter(potionRequest);
    }
    public void RecievePotion(Potion _potion)
    {
        //check if potion is correct
        //if correct
        if(_potion.GetPotionType() == potionRequest.potionType)
        {
            Events.OnPotionDelivered();
        }
    }
}
