using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI uiText;

    [Header("Timer")]
    [SerializeField] TimerSystem timer;
    [SerializeField] Image timerUI;
    
    [SerializeField] Color startColor, midColor, endColor;

    void OnEnable()
    {
        Events.OnSuccessfulBrew += SuccessText;
        Events.OnFailedBrew += FailText;

        Events.OnTimeUp += LoseScreen;
    }
    void OnDisable()
    {
        Events.OnSuccessfulBrew -= SuccessText;
        Events.OnFailedBrew -= FailText;
    
        Events.OnTimeUp -= LoseScreen;
    }
    void Update()
    {
        timerUI.fillAmount = timer.GetTimePercentage();
        
        ChangeTimerColor(); 
    }
    
    void SuccessText(PotionSO _potionSO)
    {
        uiText.text = "Successfully Brewed: " + _potionSO.name;
    }
    void FailText()
    {
        uiText.text = "Failed Brew";
    }

    void WinScreen()
    {

    }
    void LoseScreen()
    {
        uiText.text = "GAME OVER";
    }

    void ChangeTimerColor()
    {
        if(timerUI.fillAmount > 0.7f)
        {
            timerUI.color = startColor;
        }
        else if(timerUI.fillAmount > 0.3f)
        {
            timerUI.color = midColor;
        }
        else 
        {
            timerUI.color = endColor;
        }
    }
}
