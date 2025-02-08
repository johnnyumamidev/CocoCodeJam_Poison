using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Customer : MonoBehaviour
{
    [SerializeField] List<PotionSO> potionsNeeded = new List<PotionSO>();
    public int minSymptoms = 0;

    [SerializeField] AudioSource SFX;
    [SerializeField] AudioClip healClip;
    
    [Header("Dialogue")]
    [SerializeField] GameObject speechBubble;
    [SerializeField] TextMeshProUGUI dialogue;
    bool displaySymptomText = true;

    [Header("Movement")]
    bool isMoving = false;
    bool isLeaving = false;
    [SerializeField] float moveSpeed = 5f;
    Vector3 moveTarget;
    
    void Start()
    {
        speechBubble.SetActive(false);

        // randomly determine how many symptoms they have
        InitializeSymptoms();

        //go through list of potions and generate sprite for all the different symptoms
        UpdateSprites();
        DetermineFX();
    }
    void Update()
    {
        if(isMoving)
        {
            MoveToTarget();
        }

        UpdateDialogue();
    }
    
    #region public methods
    public void RecievePotion(Potion _potion)
    {
        //check if potion helps their current symptom
        if(_potion.GetPotionType() == potionsNeeded[0].potionType)
        {
            potionsNeeded.Remove(potionsNeeded[0]);
            //dialogue event saying "oh...that's better..."
        }
        else
        {
            StartCoroutine(DisplayUnsatisfiedDialogue());
        }

        DetermineFX();
        //update visuals based on cured symptoms
        if(potionsNeeded.Count > 0)
        {
            UpdateSprites();
        }
            
        //if potions needed has been depleted, change customer
        else
        {
            SFX.PlayOneShot(healClip);
            Events.OnSymptomsCured();

            //update sprite to happy!
            UpdateSpritesToSatisfied();

            //TODO delay event call until dialogue has been displayed and hidden
            StartCoroutine(DelayCustomerLeave());
        }

        StartCoroutine(DialoguePopUpEffect());
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
                speechBubble.SetActive(true);
                StartCoroutine(DialoguePopUpEffect());
            }
            isMoving = false;
        }
    }
    #endregion
    
    #region dialogue
    IEnumerator DialoguePopUpEffect()
    {
        speechBubble.transform.localScale = Vector3.one * 1.025f;
        yield return new WaitForSeconds(0.1f);
        speechBubble.transform.localScale = Vector3.one;
    }
    void UpdateDialogue()
    {
        if(potionsNeeded.Count > 0)
        {
            if(displaySymptomText)
            {
                dialogue.text = potionsNeeded[0].symptomDialogue;
            }
            else
            {
                dialogue.text = "That didn't help at all!";
            }
        }
        else 
        {
            StartCoroutine(DialoguePopUpEffect());
            dialogue.text = "Thanks I feel much better now!";
        }
    }

    IEnumerator DisplayUnsatisfiedDialogue()
    {
        float displayDuration = 1.25f;
        //dialogue says "That's not helping!"
        displaySymptomText = false;
        yield return new WaitForSeconds(displayDuration);
        displaySymptomText = true;
    }

    IEnumerator DelayCustomerLeave()
    {
        float duration = 1.5f;
        yield return new WaitForSeconds(duration);
        Events.OnCustomerLeaving();
    }
    #endregion
    
    void InitializeSymptoms()
    {
        // TODO make max symptoms dynamic and variable based on how far in the game you are
        //  example: easy mode max = 2, hard = 5
        int numberOfSymptoms = Random.Range(minSymptoms+1, minSymptoms + 2);

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
    public GameObject happyFace;

    //Arm Sprites
    public GameObject headAcheArms;
    public GameObject chillsArms;

    //FX Sprites
    public GameObject chillsFX;
    public GameObject headAcheFX;
    public GameObject feverFX;
    public GameObject nauseaFX;
    
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
        nauseaFX.SetActive(false);

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
                    nauseaFX.SetActive(true);
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

    void UpdateSprites()
    {
        DetermineFaceSprite();
        DetermineArmsSprite();
    }

    void UpdateSpritesToSatisfied()
    {
        barfFace.SetActive(false);
        dizzyFace.SetActive(false);
        painFace.SetActive(false);
        neutralFace.SetActive(false);
        headAcheArms.SetActive(false);
        chillsArms.SetActive(false);

        happyFace.SetActive(true);
    }
}