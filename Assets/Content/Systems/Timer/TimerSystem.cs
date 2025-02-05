using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnCustomerReady += StartTimer;
        Events.OnSymptomsCured += ResetTimer;
    }
    void OnDisable()
    {
        Events.OnCustomerReady -= StartTimer;
        Events.OnSymptomsCured -= ResetTimer;
    }

    bool customerReady = false;
    [SerializeField] float maxTime = 2f;
    float currentTime;
    void Start()
    {
        ResetTimer();
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
