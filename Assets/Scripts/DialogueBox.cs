using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text dialogueText;
    
    public void ShowMessage(string text)
    {
        panel.SetActive(true);
        dialogueText.text = text;
        
        Invoke(nameof(HideMessage), 3f);
    }
    
    private void HideMessage()
    {
        panel.SetActive(false);
    }
}
