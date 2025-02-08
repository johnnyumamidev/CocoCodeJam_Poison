using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 1;

        Events.OnTimeUp += GameOver;
    }
    void OnDisable()
    {
        Events.OnTimeUp -= GameOver;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public GameObject gameOverScreen;
    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    //TODO GAME LOOP
    //  HEALTH SYSTEM: player has a limited number of "health points"
    //      health is depleted when they serve the wrong potion or attempt to brew a potion and fail

    // TIMER SYSTEM: provide the potion within the time limit before the patient gets sick
    
    // INCREASE CHALLENGE: customers can have multiple illnesses and require multiple different potions
}