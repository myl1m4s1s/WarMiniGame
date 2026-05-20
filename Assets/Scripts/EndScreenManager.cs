using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public CanvasGroup endPanel;
    public float fadeDuration = 2f;
    
    public void ShowEndScreen()
    {
        StartCoroutine(FadeIn());
    }
    
    private System.Collections.IEnumerator FadeIn()
    {
        endPanel.blocksRaycasts = true;
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            endPanel.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        
        endPanel.alpha = 1f;
        Time.timeScale = 0f;
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("New_game");
    }
}