using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public DoorController door;
    public Sprite leverActiveSprite;
    public Sprite leverInactiveSprite;
    private SpriteRenderer spriteRenderer;
    private bool isActive = false;
    public GameObject interactPanel;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverInactiveSprite;
        interactPanel.SetActive(false);
    }

    private void Update()
    {
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
            interactPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isActive = !isActive;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactPanel.SetActive(false);
        }
    }
}
