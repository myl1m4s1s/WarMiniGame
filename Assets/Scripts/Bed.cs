using UnityEngine;
using System.Collections;

public class Bed : MonoBehaviour
{
    public enum Season { Spring, Summer, Fall, Winter }
    public Season currentSeason = Season.Spring;
    
    public Interactable[] allTasks;
    public CanvasGroup fadePanel;
    
    private bool _playerNear;
    private SpriteRenderer _bedSprite;
    private Color _originalBedColor;
    
    void Start()
    {
        _bedSprite = GetComponent<SpriteRenderer>();
        if (_bedSprite != null)
        {
            _originalBedColor = _bedSprite.color;
        }
        
        if (fadePanel != null)
        {
            fadePanel.alpha = 0f;
            fadePanel.blocksRaycasts = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = true;
            if (_bedSprite != null)
                _bedSprite.color = Color.yellow;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = false;
            if (_bedSprite != null)
                _bedSprite.color = _originalBedColor;
        }
    }
    
    void Update()
    {
        if (_playerNear && Input.GetKeyDown(KeyCode.W))
        {
            TrySleep();
        }
    }
    
    private bool AreAllTasksDone()
    {
        if (allTasks == null || allTasks.Length == 0)
        {
            Debug.LogError("No tasks assigned to Bed!");
            return false;
        }
        
        foreach (Interactable task in allTasks)
        {
            if (task != null && !task.isDone)
            {
                return false;
            }
        }
        return true;
    }
    
    private void TrySleep()
    {
        if (currentSeason == Season.Winter)
        {
            DialogueBox db = FindAnyObjectByType<DialogueBox>();
            if (db != null)
            {
                db.ShowMessage("Winter is over. The game ends at the mailbox.");
            }
            return;
        }
        
        if (AreAllTasksDone())
        {
            StartCoroutine(SleepAndAdvanceSeason());
        }
        else
        {
            DialogueBox db = FindAnyObjectByType<DialogueBox>();
            if (db != null)
            {
                db.ShowMessage("You still have chores to do!");
            }
        }
    }
    
    private IEnumerator SleepAndAdvanceSeason()
    {
        if (fadePanel != null)
        {
            yield return StartCoroutine(FadeToBlack());
        }
        
        AdvanceToNextSeason();
        
        ResetAllTasks();
        
        yield return new WaitForSeconds(0.5f);
        
        if (fadePanel != null)
        {
            yield return StartCoroutine(FadeFromBlack());
        }
        
        DialogueBox db = FindAnyObjectByType<DialogueBox>();
        if (db != null)
        {
            db.ShowMessage("You wake up in " + currentSeason.ToString() + "!");
        }
    }
    
    private void AdvanceToNextSeason()
    {
        switch (currentSeason)
        {
            case Season.Spring:
                currentSeason = Season.Summer;
                break;
            case Season.Summer:
                currentSeason = Season.Fall;
                break;
            case Season.Fall:
                currentSeason = Season.Winter;
                break;
            case Season.Winter:
                Debug.Log("Game Complete!");
                break;
        }
    }
    
    private void ResetAllTasks()
    {
        foreach (Interactable task in allTasks)
        {
            if (task != null)
            {
                task.ResetTask();
            }
        }
    }
    
    private IEnumerator FadeToBlack()
    {
        fadePanel.blocksRaycasts = true;
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadePanel.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 1f;
    }
    
    private IEnumerator FadeFromBlack()
    {
        float elapsedTime = 0f;
        float fadeDuration = 1f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadePanel.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;
    }
}