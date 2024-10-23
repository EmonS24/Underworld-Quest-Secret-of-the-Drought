using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrabbing = false;
    private Transform grabbedBlock;
    private Rigidbody2D blockRb;  // Reference to the block's Rigidbody

    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private float detectRadius = 0.5f;
    [SerializeField] private float playerPushPullSpeed = 2f;  // Speed for pushing/pulling the player
    [SerializeField] private float blockPushPullSpeed = 2f;   // Speed for pushing/pulling the block

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrabInput();
        HandleBlockMovement();
    }

    private void CheckGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isGrabbing)
            {
                // Detect if there's a block to grab
                Collider2D detectedBlock = Physics2D.OverlapCircle(transform.position, detectRadius, blockLayer);

                if (detectedBlock != null)
                {
                    isGrabbing = true;
                    grabbedBlock = detectedBlock.transform;
                    blockRb = grabbedBlock.GetComponent<Rigidbody2D>();  // Get the Rigidbody of the block

                    // Reset block physics
                    blockRb.isKinematic = false;  // Allow movement when grabbed
                    blockRb.constraints = RigidbodyConstraints2D.FreezeRotation;  // Freeze rotation

                    PlayerVar.isGrabbing = true;
                    Debug.Log("Block grabbed: " + detectedBlock.name);
                }
                else
                {
                    Debug.Log("No block detected.");
                }
            }
            else
            {
                ReleaseBlock();
            }
        }
    }

    private void HandleBlockMovement()
    {
        if (isGrabbing && grabbedBlock != null)
        {
            // Get the horizontal movement input
            float moveInput = Input.GetAxisRaw("Horizontal");

            // If the player is trying to push or pull the block
            if (moveInput != 0)
            {
                // Use blockPushPullSpeed for the block's movement
                blockRb.velocity = new Vector2(moveInput * blockPushPullSpeed, blockRb.velocity.y);  

                // Set the player's velocity based on their movement input
                Vector2 playerVelocity = new Vector2(moveInput * playerPushPullSpeed, rb.velocity.y);
                rb.velocity = playerVelocity;  // Move the player

                // Set the pushing/pulling state
                PlayerVar.isPushing = moveInput > 0; // Set to true if moving right
                PlayerVar.isPulling = moveInput < 0;  // Set to true if moving left

                // Update last action in PlayerMov
                if (PlayerVar.isPulling)
                {
                    GetComponent<PlayerMov>().SetLastActionToPulling(true);
                }
                else
                {
                    GetComponent<PlayerMov>().SetLastActionToPulling(false);
                }
            }
            else
            {
                // Stop block movement when player stops moving
                blockRb.velocity = Vector2.zero;  
                // Reset pushing/pulling state
                PlayerVar.isPushing = false;
                PlayerVar.isPulling = false;
                GetComponent<PlayerMov>().SetLastActionToPulling(false);  // Reset last action
            }
        }
    }

    private void ReleaseBlock()
    {
        isGrabbing = false;
        grabbedBlock = null;
        blockRb = null;  // Reset block Rigidbody

        PlayerVar.isGrabbing = false;
        PlayerVar.isPushing = false; // Reset pushing state
        PlayerVar.isPulling = false; // Reset pulling state
        Debug.Log("Block released.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
