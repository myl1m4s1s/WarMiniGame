using UnityEngine;

public class Window : Interactable
{
    public GameObject windowViewSprite;
    
    public Sprite springView;
    public Sprite summerView;
    public Sprite fallView;
    public Sprite winterView;
    
    private SpriteRenderer viewRenderer;
    
    void Awake()
    {
        if (windowViewSprite != null)
        {
            viewRenderer = windowViewSprite.GetComponent<SpriteRenderer>();
            UpdateWindowView();
        }
    }
    
    public override void Interact()
    {
        base.Interact();
        
        UpdateWindowView();
        
        if (windowViewSprite != null)
        {
            windowViewSprite.SetActive(!windowViewSprite.activeSelf);
        }
    }
    
    private void UpdateWindowView()
    {
        Bed bed = FindAnyObjectByType<Bed>();
        if (bed != null && viewRenderer != null)
        {
            switch (bed.currentSeason)
            {
                case Bed.Season.Spring:
                    if (springView != null)
                        viewRenderer.sprite = springView;
                    break;
                case Bed.Season.Summer:
                    if (summerView != null)
                        viewRenderer.sprite = summerView;
                    break;
                case Bed.Season.Fall:
                    if (fallView != null)
                        viewRenderer.sprite = fallView;
                    break;
                case Bed.Season.Winter:
                    if (winterView != null)
                        viewRenderer.sprite = winterView;
                    break;
            }
        }
    }
}