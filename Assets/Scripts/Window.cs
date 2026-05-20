using UnityEngine;
using System.Collections;

public class Window : Interactable
{
    public GameObject windowImagePanel;
    public UnityEngine.UI.Image windowImage;
    
    public Sprite springImage;
    public Sprite summerImage;
    public Sprite fallImage;
    public Sprite winterImage;
    
    private bool isPanelOpen = false;
    
    public override void Interact()
    {
        if (isDone) return;
        
        OpenWindowPanel();
    }
    
    void Update()
    {
        if (isPanelOpen && Input.GetKeyDown(KeyCode.W))
        {
            CloseWindowPanel();
        }
    }
    
    private void OpenWindowPanel()
    {
        UpdateWindowImage();
        
        if (windowImagePanel != null)
        {
            windowImagePanel.SetActive(true);
            isPanelOpen = true;
            Time.timeScale = 0f;
        }
    }
    
    private void CloseWindowPanel()
    {
        if (windowImagePanel != null)
        {
            windowImagePanel.SetActive(false);
            isPanelOpen = false;
            Time.timeScale = 1f;
            
            isDone = true;
            
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
            
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
                sprite.color = _originalColor;
            
            StartCoroutine(ShowMessageWithDelay());
        }
    }
    
    private IEnumerator ShowMessageWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        
        DialogueBox db = FindAnyObjectByType<DialogueBox>();
        if (db != null && !string.IsNullOrEmpty(currentMessage))
        {
            db.ShowMessage(currentMessage);
        }
    }
    
    private void UpdateWindowImage()
    {
        Bed bed = FindAnyObjectByType<Bed>();
        if (bed != null && windowImage != null)
        {
            switch (bed.currentSeason)
            {
                case Bed.Season.Spring:
                    if (springImage != null)
                        windowImage.sprite = springImage;
                    break;
                case Bed.Season.Summer:
                    if (summerImage != null)
                        windowImage.sprite = summerImage;
                    break;
                case Bed.Season.Fall:
                    if (fallImage != null)
                        windowImage.sprite = fallImage;
                    break;
                case Bed.Season.Winter:
                    if (winterImage != null)
                        windowImage.sprite = winterImage;
                    break;
            }
        }
    }
}