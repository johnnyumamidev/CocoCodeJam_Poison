using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] List<PotionSO> potionsNeeded = new List<PotionSO>();
    [SerializeField] TextMeshProUGUI dialogue;
    void Update()
    {
        if(potionsNeeded.Count > 0)
            dialogue.text = "I need a " + potionsNeeded[0].name;
        else 
            dialogue.text = "Thanks I feel much better now!";
    }
    
    public void RecievePotion(Potion _potion)
    {
        //check if potion is one of the needed potions
        foreach(PotionSO potion in potionsNeeded)
        {
            if(_potion.GetPotionType() == potion.potionType)
            {
                potionsNeeded.Remove(potion);
                break;
            }
        }

        //if potions needed has been depleted, change customer
        if(potionsNeeded.Count <= 0)
        {
            Events.OnSymptomsCured();
        }
    }
}
