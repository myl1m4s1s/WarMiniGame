using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string springMessage;
    [TextArea]
    public string summerMessage;
    [TextArea]
    public string fallMessage;
    [TextArea]
    public string winterMessage;
    
    private bool _playerNear;
    private SpriteRenderer _objectSprite;
    private Color _originalColor;
    private DialogueBox _dialogueBox;
    private string currentMessage;
    
    protected GameObject Player;

    public bool isDone;
    public string taskName;
    
    void Start()
    {
        _objectSprite = GetComponent<SpriteRenderer>();
        if (_objectSprite != null)
        {
            _originalColor = _objectSprite.color;
        }
        
        Player = GameObject.FindGameObjectWithTag("Player");
        SetupMessageUI();
        UpdateSeasonalMessage();
    }
    
    void SetupMessageUI()
    {
        _dialogueBox = FindAnyObjectByType<DialogueBox>();
    }
    
    void Update()
    {
        if (_playerNear && Input.GetKeyDown(KeyCode.W) && !isDone)
        {
            Interact();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = true;
            if (_objectSprite != null)
                _objectSprite.color = Color.yellow;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNear = false;
            if (_objectSprite != null)
                _objectSprite.color = _originalColor;
        }
    }
    
    public virtual void Interact()
    {
        if (isDone) return;
        
        if (_dialogueBox != null)
            _dialogueBox.ShowMessage(currentMessage);
        
        isDone = true;
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        
        if (_objectSprite != null)
            _objectSprite.color = _originalColor;
    }
    
    public void ResetTask()
    {
        isDone = false;
        
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = true;
        
        if (_objectSprite != null)
            _objectSprite.color = _originalColor;
        
        UpdateSeasonalMessage();
    }
    
    public void UpdateSeasonalMessage()
    {
        Bed bed = FindAnyObjectByType<Bed>();
        if (bed != null)
        {
            switch (bed.currentSeason)
            {
                case Bed.Season.Spring:
                    if (!string.IsNullOrEmpty(springMessage))
                        currentMessage = springMessage;
                    break;
                case Bed.Season.Summer:
                    if (!string.IsNullOrEmpty(summerMessage))
                        currentMessage = summerMessage;
                    else
                        currentMessage = springMessage;
                    break;
                case Bed.Season.Fall:
                    if (!string.IsNullOrEmpty(fallMessage))
                        currentMessage = fallMessage;
                    else
                        currentMessage = springMessage;
                    break;
                case Bed.Season.Winter:
                    if (!string.IsNullOrEmpty(winterMessage))
                        currentMessage = winterMessage;
                    else
                        currentMessage = springMessage;
                    break;
            }
        }
    }
}