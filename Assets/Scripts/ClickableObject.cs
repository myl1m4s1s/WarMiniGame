using TMPro;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public string prompt;
    public bool wasClicked;

    public GameObject viewToOpen; // assign WindowView or MailboxView

    public void Click(TMP_Text text)
    {
        if (wasClicked) return;

        wasClicked = true;
        text.text = prompt;

        if (viewToOpen != null)
        {
            viewToOpen.SetActive(true);
        }
    }
}