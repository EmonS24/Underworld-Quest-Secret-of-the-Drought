using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;                                
    private bool isGrounded; 
    public float fallMultiplier;
    public float lowJumpMultiplier;         
    public Transform groundCheck; // Position to check for ground
    public LayerMask groundLayer; // Layer mask for ground detection
    private float groundCheckRadius = 0.2f; // Radius for ground detection               

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrounded();
        HandleJump();
    }

    void HandleJump()
    {
        // Apply gravity multipliers for better jump control
        if (rb.velocity.y < 0)
        {
             rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // End the jump if the space key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        // Perform the initial jump when on the ground and space is pressed
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            // Add the horizontal velocity (for moving jumps)
            Vector2 jumpVelocity = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVelocity;
        }

        // Allow holding space for a higher jump
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Maintain horizontal velocity while jumping
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
    }


    void CheckGrounded()
    {
        // Check if the player is on the ground using a layer mask
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
