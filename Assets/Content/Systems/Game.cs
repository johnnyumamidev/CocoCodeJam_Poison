using UnityEngine;

public class Game : MonoBehaviour
{
   
    //Handle customers coming and going into the shop
    void OnEnable()
    {
        Events.OnPotionDelivered += ChangeCustomer;
    }
    void OnDisable()
    {
        Events.OnPotionDelivered -= ChangeCustomer;
    }

    void ChangeCustomer()
    {
        //Swap out current customer with new customer
        Debug.Log("Change Customer");
    }

    //TODO GAME LOOP
    //  HEALTH SYSTEM: player has a limited number of "health points"
    //      health is depleted when they serve the wrong potion or attempt to brew a potion and fail

    // TIMER SYSTEM: provide the potion within the time limit before the patient gets sick
    
    // INCREASE CHALLENGE: customers can have multiple illnesses and require multiple different potions
}
