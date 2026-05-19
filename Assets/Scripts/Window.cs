using UnityEngine;

public class Window : Interactable
{
    public GameObject windowImagePanel;
    public UnityEngine.UI.Image windowImage;
    
    public Sprite springImage;
    public Sprite summerImage;
    public Sprite fallImage;
    public Sprite winterImage;
    
    public float displayDuration = 3f;
    
    public override void Interact()
    {
        base.Interact();
        
        UpdateWindowImage();
        ShowWindowImage();
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
    
    private void ShowWindowImage()
    {
        if (windowImagePanel != null)
        {
            windowImagePanel.SetActive(true);
            Invoke(nameof(HideWindowImage), displayDuration);
        }
    }
    
    private void HideWindowImage()
    {
        if (windowImagePanel != null)
        {
            windowImagePanel.SetActive(false);
        }
    }
}