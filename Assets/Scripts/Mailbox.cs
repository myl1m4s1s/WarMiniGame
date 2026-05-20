using UnityEngine;

public class Mailbox : Interactable
{
    public GameObject mailImagePanel;
    public UnityEngine.UI.Image mailImage;
    public CanvasGroup endScreenPanel;
    
    public Sprite winterMailSprite;
    public Sprite springMailSprite;
    public Sprite summerMailSprite;
    public Sprite fallMailSprite;
    
    private bool isPanelOpen = false;
    
    public override void Interact()
    {
        Bed bed = FindAnyObjectByType<Bed>();
        
        if (bed != null && bed.currentSeason == Bed.Season.Winter)
        {
            OpenMailPanelForEnding();
            return;
        }
        
        if (isDone) return;
        
        OpenMailPanel();
    }
    
    private void OpenMailPanelForEnding()
    {
        UpdateMailImage();
        
        if (mailImagePanel != null)
        {
            mailImagePanel.SetActive(true);
            isPanelOpen = true;
            Time.timeScale = 0f;
        }
    }
    
    void Update()
    {
        if (isPanelOpen && Input.GetKeyDown(KeyCode.W))
        {
            CloseMailPanel();
        }
    }
    
    private void OpenMailPanel()
    {
        UpdateMailImage();
        
        if (mailImagePanel != null)
        {
            mailImagePanel.SetActive(true);
            isPanelOpen = true;
            Time.timeScale = 0f;
        }
    }
    
    private void CloseMailPanel()
    {
        if (mailImagePanel != null)
        {
            mailImagePanel.SetActive(false);
            isPanelOpen = false;
            Time.timeScale = 1f;
            
            isDone = true;
            
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
            
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
                sprite.color = _originalColor;
            
            DialogueBox db = FindAnyObjectByType<DialogueBox>();
            if (db != null && !string.IsNullOrEmpty(currentMessage))
            {
                db.ShowMessage(currentMessage);
            }
            
            Bed bed = FindAnyObjectByType<Bed>();
            if (bed != null && bed.currentSeason == Bed.Season.Winter)
            {
                StartCoroutine(FadeToEnd());
            }
        }
    }
    
    private System.Collections.IEnumerator FadeToEnd()
    {
        if (endScreenPanel == null) yield break;
        
        yield return new WaitForSeconds(3f);
        
        endScreenPanel.blocksRaycasts = true;
        float elapsedTime = 0f;
        float fadeDuration = 2f;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            endScreenPanel.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
        
        endScreenPanel.alpha = 1f;
        Time.timeScale = 0f;
    }
    
    private void UpdateMailImage()
    {
        Bed bed = FindAnyObjectByType<Bed>();
        if (bed != null && mailImage != null)
        {
            switch (bed.currentSeason)
            {
                case Bed.Season.Spring:
                    if (springMailSprite != null)
                        mailImage.sprite = springMailSprite;
                    break;
                case Bed.Season.Summer:
                    if (summerMailSprite != null)
                        mailImage.sprite = summerMailSprite;
                    break;
                case Bed.Season.Fall:
                    if (fallMailSprite != null)
                        mailImage.sprite = fallMailSprite;
                    break;
                case Bed.Season.Winter:
                    if (winterMailSprite != null)
                        mailImage.sprite = winterMailSprite;
                    break;
            }
        }
    }
}