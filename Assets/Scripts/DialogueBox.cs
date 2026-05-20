using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text dialogueText;
    public float displayDuration = 3f;
    
    public void ShowMessage(string text)
    {
        panel.SetActive(true);
        dialogueText.text = text;
        
        CancelInvoke(nameof(HideMessage));
        Invoke(nameof(HideMessage), displayDuration);
    }
    
    private void HideMessage()
    {
        panel.SetActive(false);
    }
}