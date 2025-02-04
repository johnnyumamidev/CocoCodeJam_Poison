using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PotionSO", menuName = "PotionSO", order = 0)]
public class PotionSO : ScriptableObject
{
    public List<ItemType> itemsRequiredToBrew = new();
    public GameObject prefab;
    public PotionType potionType;
}

public enum PotionType
{
    Red, Blue, Green, Yellow
}
