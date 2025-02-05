using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnCustomerReady += StartTimer;
        Events.OnSymptomsCured += StopTimer;
    }
    void OnDisable()
    {
        Events.OnCustomerReady -= StartTimer;
        Events.OnSymptomsCured -= StopTimer;
    }

    bool customerReady = false;
    [SerializeField] float maxTime = 2f;
    float currentTime;
    
    void Start()
    {
        currentTime = maxTime;
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
    }
    void StopTimer()
    {

    }
    void ResetTimer()
    {

    }
}
