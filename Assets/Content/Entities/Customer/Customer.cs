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
    
    void Start()
    {
        // randomly determine how many symptoms they have
        InitializeSymptoms();

        //go through list of potions and generate sprite for all the different symptoms
        GenerateVisuals();

    }
    void Update()
    {
        if(isMoving)
        {
            MoveToTarget();
        }

        // DISPLAY WHAT POTION THEY NEED
        // REFACTOR: make dialogue display patient symptoms rather than what color potion they want
        //  example: "i have chills" "i feel like throwing up" "my head is pounding"
            if(potionsNeeded.Count > 0)
                dialogue.text = potionsNeeded[0].symptomDialogue;
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

        //update visuals based on cured symptoms

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

    void InitializeSymptoms()
    {
        // TODO make max symptoms dynamic and variable based on how far in the game you are
        //  example: easy mode max = 2, hard = 5
        int maxSymptoms = 5;
        int numberOfSymptoms = Random.Range(1, maxSymptoms);

        //get list of all possible potions
        List<PotionSO> potions = new List<PotionSO>(FindObjectOfType<PotionRecipes>().GetPotionRecipes());
        for(int i = 0; i < numberOfSymptoms; i++)
        {
            potionsNeeded.Add(potions[Random.Range(0, potions.Count)]);
        }
    }
    void GenerateVisuals()
    {
        
    }
}