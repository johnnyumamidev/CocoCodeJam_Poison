using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI uiText;

    [Header("Timer")]
    [SerializeField] TimerSystem timer;
    [SerializeField] Image timerUI;

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
}
