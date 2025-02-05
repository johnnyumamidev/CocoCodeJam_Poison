using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    
    void OnEnable()
    {
        Events.OnSuccessfulBrew += SpawnPotion;
    }
    void OnDisable()
    {
        Events.OnSuccessfulBrew -= SpawnPotion;
    }

    void SpawnPotion(PotionSO potionSO)
    {
        GameObject potionObj = Instantiate(potionSO.prefab, spawnPoint.position, Quaternion.identity);
        Potion potionScript = potionObj.GetComponent<Potion>();

        potionScript.SetPotionType(potionSO);
    }
}
