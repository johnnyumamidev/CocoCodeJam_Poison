using UnityEngine;
using System.Collections.Generic;

public class PotionRecipes : MonoBehaviour
{
    [SerializeField] List<PotionSO> potionRecipes = new List<PotionSO>();

    public List<PotionSO> GetPotionRecipes()
    {
        return potionRecipes;
    }
}
