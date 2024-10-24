using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = false; 
    }

    void Update()
    {
        CheckGrounded();
        HandleJump();
    }

    public void HandleJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (PlayerVar.isGrounded && Input.GetKeyDown(KeyCode.Space) && !PlayerVar.isCrouching)
        {
            PlayerVar.isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // hold jump
        if (Input.GetKeyDown(KeyCode.Space) && PlayerVar.isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                PlayerVar.isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlayerVar.isJumping = false;
        }
    }


    public void CheckGrounded()
    {
        PlayerVar.isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);

        if (PlayerVar.isGrounded)
        {
            PlayerVar.isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRadius);
    }
}
