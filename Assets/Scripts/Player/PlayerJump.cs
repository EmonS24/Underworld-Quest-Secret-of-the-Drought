using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;
    private PlayerVar player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerVar>();
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
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !player.isCrouching && !player.isGrabbing)
        {
            player.isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void CheckGrounded()
    {
        player.isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckRadius, groundLayer);

        if (player.isGrounded)
        {
            player.isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckRadius);
    }
}
