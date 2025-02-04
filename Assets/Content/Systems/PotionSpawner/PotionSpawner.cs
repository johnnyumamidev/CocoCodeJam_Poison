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
        GameObject potion = Instantiate(potionSO.prefab, spawnPoint.position, Quaternion.identity);
    }
}
