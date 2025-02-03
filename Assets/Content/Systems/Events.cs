using UnityEngine;
using UnityEngine.Events;

public static class Events
{
    public static UnityAction<GameObject> OnItemSpawned;
    public static UnityAction<GameObject> OnItemDropped;
    public static UnityAction OnSuccessfulBrew;
    public static UnityAction OnFailedBrew;
}
