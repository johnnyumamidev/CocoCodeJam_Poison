using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] List<PotionSO> potionsNeeded = new List<PotionSO>();
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshProUGUI dialogue;

    [Header("Movement")]
    bool isMoving = false;
    bool isLeaving = false;
    [SerializeField] float moveSpeed = 5f;
    Vector3 moveTarget;
 
    void Update()
    {
        if(isMoving)
        {
            MoveToTarget();
        }

        // DISPLAY WHAT POTION THEY NEED
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

    public void SetMoveTarget(Vector3 targetPosition, bool leaving)
    {
        moveTarget = targetPosition;
        isMoving = true;

        isLeaving = leaving;
    }

    void MoveToTarget()
    {
        speechBubble.SetActive(false);

        transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed * Time.deltaTime);

        float distanceToTargetPoint = Vector3.Distance(transform.position, moveTarget);
        if(distanceToTargetPoint <= 0.1f)
        {
            if(isLeaving)
            {
                Events.OnCustomerLeft();
            }
            else
            {
                Events.OnCustomerReady();
                speechBubble.SetActive(true);
            }
            isMoving = false;
        }
    }
}
