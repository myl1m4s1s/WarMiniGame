using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private float moveInput;
    private Interactable currentInteractable;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.W) && currentInteractable != null)
        {
            currentInteractable.Interact();
            currentInteractable = null;
        }
    }
    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && !interactable.isDone)
        {
            currentInteractable = interactable;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}