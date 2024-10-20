using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public Transform ledgeCheck;       // For checking ledges
    public Transform leftWallCheck;    // For checking walls on the left
    public Transform rightWallCheck;   // For checking walls on the right
    public float ledgeCheckDistance = 0.5f;  // Distance for ledge detection
    public float wallCheckDistance = 0.5f;   // Distance for wall detection
    public LayerMask groundLayer;      // Layer for both ground and walls

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        LedgeGrabCheck();
    }

    private void LedgeGrabCheck()
    {
        // Check if the player is next to a wall
        bool isNextToWall = Physics2D.Raycast(leftWallCheck.position, Vector2.left, wallCheckDistance, groundLayer) ||
                            Physics2D.Raycast(rightWallCheck.position, Vector2.right, wallCheckDistance, groundLayer);
        
        // Check if there's a ledge above (use upward ray)
        bool isLedgeAbove = Physics2D.Raycast(ledgeCheck.position, Vector2.up, ledgeCheckDistance, groundLayer);

        // Debugging outputs
        Debug.Log($"Is next to wall: {isNextToWall}, Is ledge above: {isLedgeAbove}, Is jumping: {PlayerVar.isJumping}");

        if (isNextToWall && !isLedgeAbove && PlayerVar.isJumping)
        {
            // Trigger climbing when jumping into a ledge
            ClimbLedge();
        }
    }

    private void ClimbLedge()
    {
        // Adjust player's position to climb over the ledge
        transform.position += new Vector3(0, 1.0f, 0); // Adjust height as necessary
        PlayerVar.isClimbing = true; // Set climbing state
        rb.velocity = Vector2.zero; // Stop movement when climbing

        // Debugging confirmation
        Debug.Log("Climbing ledge!");
    }

    private void OnDrawGizmos()
    {
        // Visualize ledge check upward
        Gizmos.color = Color.green;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + Vector3.up * ledgeCheckDistance);

        // Visualize wall checks
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftWallCheck.position, leftWallCheck.position + Vector3.left * wallCheckDistance); // Left wall check
        Gizmos.DrawLine(rightWallCheck.position, rightWallCheck.position + Vector3.right * wallCheckDistance); // Right wall check
    }
}
