using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    public float moveSpeed = 2f; // Movement speed when pushing/pulling
    private bool isGrabbed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Initially stop the block from moving
    }

    public void Grab(Transform playerTransform)
    {
        player = playerTransform;
        isGrabbed = true;
        rb.isKinematic = false; // Allow the block to be pushed/pulled
    }

    public void Release()
    {
        isGrabbed = false;
        player = null;
        rb.velocity = Vector2.zero; // Stop the block immediately
        rb.isKinematic = true; // Stop the block when released
    }

    void Update()
    {
        if (isGrabbed)
        {
            // Move the block with the player
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    }
}
