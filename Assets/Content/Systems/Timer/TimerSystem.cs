using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    bool customerReady = false;
    [SerializeField] float maxTime = 2f;
    float currentTime;
    void Start()
    {
        StartTimer();
    }
    void Update()
    {
        if(customerReady)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                Events.OnTimeUp();
            }
        }
    }
    
    public float GetTimePercentage()
    {
        return currentTime / maxTime;
    }

    void StartTimer()
    {
        ResetTimer();
        customerReady = true;
    }

    void ResetTimer()
    {
        currentTime = maxTime;
        customerReady = false;
    }
}
