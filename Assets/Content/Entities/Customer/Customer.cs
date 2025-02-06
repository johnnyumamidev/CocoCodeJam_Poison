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
        UpdateSymptoms();
        DetermineFX();
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
    
    #region public methods
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
        if(potionsNeeded.Count > 0)
        {
            UpdateSymptoms();
        }
            
        //if potions needed has been depleted, change customer
        else
        {
            Events.OnSymptomsCured();
        }
    }
    #endregion

    #region movement
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
    #endregion
    
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
            PotionSO potionToAdd = potions[Random.Range(0, potions.Count)]; 
            if(!potionsNeeded.Contains(potionToAdd))
                potionsNeeded.Add(potionToAdd);
        }
    }
   
    [Header("Visuals")]
    //Face Sprites
    public GameObject neutralFace;
    public GameObject barfFace;
    public GameObject painFace;
    public GameObject dizzyFace;

    //Arm Sprites
    public GameObject headAcheArms;
    public GameObject chillsArms;

    //FX Sprites
    public GameObject chillsFX;
    public GameObject headAcheFX;
    public GameObject feverFX;
    
    void DetermineFaceSprite()
    {
        PotionType type = potionsNeeded[0].potionType;
        switch(type)
        {
            case PotionType.Red:
                barfFace.SetActive(false);
                dizzyFace.SetActive(false);
                painFace.SetActive(false);
                neutralFace.SetActive(true);
                break;
            case PotionType.Green:
                painFace.SetActive(false);
                dizzyFace.SetActive(false);
                neutralFace.SetActive(false);
                barfFace.SetActive(true);
                break;
            case PotionType.Blue:
                barfFace.SetActive(false);
                dizzyFace.SetActive(false);
                neutralFace.SetActive(false);
                painFace.SetActive(true);
                break;
            case PotionType.Yellow:
                barfFace.SetActive(false);
                dizzyFace.SetActive(false);
                neutralFace.SetActive(false);
                painFace.SetActive(true);
                break;
            case PotionType.Purple:
                barfFace.SetActive(false);
                dizzyFace.SetActive(false);
                painFace.SetActive(false);
                neutralFace.SetActive(true);
                break;
            case PotionType.Orange:
                barfFace.SetActive(false);
                neutralFace.SetActive(false);
                painFace.SetActive(false);
                dizzyFace.SetActive(true);
                break;
        }
    }

    void DetermineArmsSprite()
    {
        //disable all arms
        headAcheArms.SetActive(false);
        chillsArms.SetActive(false);

        PotionType type = potionsNeeded[0].potionType;
        switch(type)
        {
            case PotionType.Red:
                headAcheArms.SetActive(false);
                chillsArms.SetActive(false);
                break;
            case PotionType.Green:
                headAcheArms.SetActive(false);
                chillsArms.SetActive(false);
                break;
            case PotionType.Blue:
                headAcheArms.SetActive(true);
                chillsArms.SetActive(false);
                break;
            case PotionType.Yellow:
                headAcheArms.SetActive(false);
                chillsArms.SetActive(false);
                break;
            case PotionType.Purple:
                headAcheArms.SetActive(false);
                chillsArms.SetActive(true);
                break;
            case PotionType.Orange:
                headAcheArms.SetActive(false);
                chillsArms.SetActive(false);
                break;
        }
    }
    void DetermineFX()
    {
        //Disable all FX
        feverFX.SetActive(false);
        chillsFX.SetActive(false);
        headAcheFX.SetActive(false);

        foreach(PotionSO potion in potionsNeeded)
        {
            PotionType type = potion.potionType;
            switch(type)
            {
            //Fever
                case PotionType.Red:
                    feverFX.SetActive(true);
                    break;
            //Nausea
                case PotionType.Green:
                    break;
            //headache
                case PotionType.Blue:
                    headAcheFX.SetActive(true);
                    break;
            //muscle spasm
                case PotionType.Yellow:
                    break;
            //chills
                case PotionType.Purple:
                    chillsFX.SetActive(true);
                    break;
            //dizziness
                case PotionType.Orange:
                    break;
            }
        }
    }

    void UpdateSymptoms()
    {
        DetermineFaceSprite();
        DetermineArmsSprite();
    }
}