using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] float maxTime = 2f;
    float currentTime;
    void Start()
    {
        currentTime = maxTime;
    }
    void Update()
    {
        currentTime -= Time.deltaTime;

        if(currentTime <= 0)
        {
            Events.OnTimeUp();
        }
    }
    
    public float GetTimePercentage()
    {
        return currentTime / maxTime;
    }
}
