using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DayManager : MonoBehaviour
{
    public TMP_Text promptText;
    public ClickableObject[] clickableObjects;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // know where the mouse is on the screen
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            
            // convert screen to world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            
            // Physics check - to check if the mouse overlapped another collider
            Collider2D hit = Physics2D.OverlapPoint(worldPosition);

            // If anything was hit
            if (hit != null && hit.TryGetComponent(out ClickableObject clickable))
            {
                clickable.Click(promptText);
                CheckIfDone();
            }
        }
    }

    private void CheckIfDone()
    {
        foreach (ClickableObject clickableObject in clickableObjects)
        {
            if (!clickableObject.wasClicked) return; // Not done - don't do anything
        }
        
        Debug.Log("All items were clicked");
    }
}