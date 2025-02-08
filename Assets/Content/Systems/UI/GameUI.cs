using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI uiText;
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    [Header("Timer")]
    [SerializeField] TimerSystem timer;
    [SerializeField] Image timerUI;
    
    [SerializeField] Color startColor, midColor, endColor;

    [Header("Game Over Screen")]
    [SerializeField] TextMeshProUGUI finalScoreText;

    void OnEnable()
    {
        Events.OnSymptomsCured += UpdateScore;

        Events.OnTimeUp += DisplayFinalScore;
    }
    void OnDisable()
    {
        Events.OnSymptomsCured += UpdateScore;

        Events.OnTimeUp -= DisplayFinalScore;
    }
    void Update()
    {
        timerUI.fillAmount = timer.GetTimePercentage();
        
        ChangeTimerColor(); 

        scoreText.text = "Patients Helped: " + score;
    }
    void DisplayFinalScore()
    {
        finalScoreText.text = score.ToString();
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

    void UpdateScore()
    {
        score++;
    }
}
