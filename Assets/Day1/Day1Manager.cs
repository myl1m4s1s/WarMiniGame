using UnityEngine;
using TMPro;

public class Day1Manager : MonoBehaviour
{
    public GameObject mailboxView;
    
    public TextMeshProUGUI promptText;
    
    public GameObject windowView;

    private int step = 0;

    void Start()
    {
        UpdatePrompt();
        windowView.SetActive(false); //hide window view at start
    }
    
    public void ClickPlant()
    {
        if (step == 0) NextStep();
    }
    
    public void ClickRadio()
    {
        if (step == 1) NextStep();
    }
    
    public void ClickWindow()
    {
        if (step == 2)
        {
            windowView.SetActive(true);
        }
    }
    
    public void ClickMailbox()
    {
        if (step == 3) NextStep();
        {
            mailboxView.SetActive(true);
        }   
    }
    
    public void CloseWindowView()
    {
        windowView.SetActive(false);
        NextStep();
    }
    
    
    

    void NextStep()
    {
        step++;
        UpdatePrompt();
    }

    void UpdatePrompt()
    {
        switch (step)
        {
            case 0:
                promptText.text = "The plants are looking dehydrated.";
                break;

            case 1:
                promptText.text = "I haven't heard the radio in days... maybe they've got news.";
                break;

            case 2:
                promptText.text = "Hm. Less people out every day. I can't remember the last time I left the house either...";
                break;

            case 3:
                promptText.text = "Ah! I should check the mail.";
                break;

            case 4:
                promptText.text = "Nothing today.";
                break;
        }
    }
}