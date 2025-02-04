using UnityEngine;

public class Customer : MonoBehaviour
{
    public PotionSO potionRequest;

    public void RecievePotion(Potion _potion)
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        //check if potion is correct
        //if correct
        if(_potion.GetPotionType() == potionRequest.potionType)
        {
            Events.OnPotionDelivered();

            //TEST CORRECT POTION RECEIEVED
            sr.color = Color.green;
        }
        else 
        {
            //INCORRECT POTION RECEIVED
            sr.color = Color.red;
        }
        
    }
}
