using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    //Ingredient Handling
    public static UnityAction<GameObject> OnItemSpawned;
    public static UnityAction<GameObject> OnItemDropped;

    //Potion Brewing
    public static UnityAction<PotionSO> OnSuccessfulBrew;
    public static UnityAction OnFailedBrew;
 
    //Customer Events
    public static UnityAction OnCustomerReady;
    public static UnityAction OnSymptomsCured;
    public static UnityAction OnTimeUp;
}