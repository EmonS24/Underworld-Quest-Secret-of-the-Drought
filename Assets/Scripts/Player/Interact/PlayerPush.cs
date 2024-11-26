using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    private GameObject currentObject;
    private Vector2 offset;
    private PlayerVar player;

    void Start()
    {
        player = GetComponent<PlayerVar>();
    }

    void Update()
    {
        HandleGrab();
    }

    private void HandleGrab()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.isGrounded)
        {
            if (currentObject != null)
            {
                player.isGrabbing = !player.isGrabbing;

                Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    if (player.isGrabbing)
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
                    }
                    else
                    {
                        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; 
                        currentObject.transform.position = (Vector2)transform.position + offset; 
                    }
                }
            }
        }
        if (player.isGrabbing && currentObject != null)
        {
            MoveObject();
        }
        if (!player.isGrounded)
        {
            ReleaseGrab();
        }
    }

    private void MoveObject()
    {
        Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 newPosition = (Vector2)transform.position + offset;
            rb.MovePosition(newPosition);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            currentObject = collision.gameObject;
            offset = (Vector2)(currentObject.transform.position - transform.position);
        }
    }

    private void ReleaseGrab()
    {
        if (currentObject != null)
        {
            Rigidbody2D rb = currentObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        player.isGrabbing = false;
        currentObject = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            currentObject = null;
            if (player.isGrabbing)
            {
                player.isGrabbing = false;

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
}