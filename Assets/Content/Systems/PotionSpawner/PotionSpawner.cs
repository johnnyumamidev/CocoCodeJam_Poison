using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject failText;
    
    void OnEnable()
    {
        Events.OnSuccessfulBrew += SpawnPotion;

        Events.OnFailedBrew += FailPotion;
    }
    void OnDisable()
    {
        Events.OnSuccessfulBrew -= SpawnPotion;
        
        Events.OnFailedBrew -= FailPotion;
    }

    void SpawnPotion(PotionSO potionSO)
    {
        GameObject potionObj = Instantiate(potionSO.prefab, spawnPoint.position, Quaternion.identity);
        Potion potionScript = potionObj.GetComponent<Potion>();

        potionScript.SetPotionType(potionSO);
    }

    void FailPotion()
    {
        failText.SetActive(true);
        Invoke("DisableText", 0.5f);
    }

    void DisableText()
    {
        failText.SetActive(false);
    }
}
