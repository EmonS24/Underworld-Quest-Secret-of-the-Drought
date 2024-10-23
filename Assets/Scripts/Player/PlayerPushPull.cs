using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform grabbedBlock;
    private Rigidbody2D blockRb;

    [SerializeField] private LayerMask blockLayer;
    [SerializeField] private float detectRadius = 0.5f;
    [SerializeField] private float playerPushPullSpeed = 2f;
    [SerializeField] private float blockPushPullSpeed = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGrabInput();
        HandleBlockMovement();
    }

    private void CheckGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerVar.isGrabbing)
            {
                ReleaseBlock();
            }
            else
            {
                Collider2D detectedBlock = Physics2D.OverlapCircle(transform.position, detectRadius, blockLayer);

                if (detectedBlock != null)
                {
                    GrabBlock(detectedBlock);
                }
                else
                {
                    Debug.Log("No block detected.");
                }
            }
        }
    }

    private void GrabBlock(Collider2D detectedBlock)
    {
        PlayerVar.isGrabbing = true;
        grabbedBlock = detectedBlock.transform;
        blockRb = grabbedBlock.GetComponent<Rigidbody2D>();
        blockRb.isKinematic = true;  // Prevent physics interactions
        Debug.Log("Block grabbed: " + detectedBlock.name);
    }

    private void HandleBlockMovement()
    {
        if (PlayerVar.isGrabbing && grabbedBlock != null)
        {
            // Get movement input
            float moveInput = Input.GetAxisRaw("Horizontal");

            // Move the player
            Vector2 playerVelocity = new Vector2(moveInput * playerPushPullSpeed, rb.velocity.y);
            rb.velocity = playerVelocity;

            // Move the block
            if (moveInput != 0)
            {
                blockRb.MovePosition(grabbedBlock.position + new Vector3(moveInput * blockPushPullSpeed * Time.deltaTime, 0f, 0f));
            }
        }
    }

    private void ReleaseBlock()
    {
        if (grabbedBlock != null)
        {
            blockRb.isKinematic = false;  // Restore physics interactions
            PlayerVar.isGrabbing = false;
            grabbedBlock = null;
            blockRb = null;
            Debug.Log("Block released.");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
