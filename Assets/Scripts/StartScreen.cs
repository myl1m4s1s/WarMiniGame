using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startPanel;
    
    void Start()
    {
        Time.timeScale = 0f;  // Pauses the game
        startPanel.SetActive(true);
    }
    
    public void StartGame()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f;  // Unpauses the game
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}