using UnityEngine;

public class Lever : MonoBehaviour
{
    public DoorController door;
    public Sprite leverActiveSprite;
    public Sprite leverInactiveSprite;

    [Header("Audio")]
    public AudioClip leverSound; 
    public AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private bool isActive = false;
    private bool isPlayerInTrigger = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = leverInactiveSprite;
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLever();
            if (leverSound != null)
            {
                audioSource.PlayOneShot(leverSound); 
            }
        }
    }

    private void ToggleLever()
    {
        isActive = !isActive;

        if (isActive)
        {
            spriteRenderer.sprite = leverActiveSprite;
            door.OpenDoor();
        }
        else
        {
            spriteRenderer.sprite = leverInactiveSprite;
            door.CloseDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
