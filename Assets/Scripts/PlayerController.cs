using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private float moveInput;
    private Interactable currentInteractable;
    private Animator animator;
    private float originalYScale;
    private float originalZScale;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalYScale = transform.localScale.y;
        originalZScale = transform.localScale.z;
    }
    
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
        }
        
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1, originalYScale, originalZScale);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1, originalYScale, originalZScale);
        }
        
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