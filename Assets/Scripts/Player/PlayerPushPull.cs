using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushPull : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrabbing = false;
    private bool isPushing = false;
    private bool isPulling = false;

    [SerializeField] private float pushPullSpeed = 2f;
    [SerializeField] private LayerMask blockLayer;

    private Collider2D blockCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrabInput();
        HandleMovement();
    }

    private void CheckGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D detectedBlock = Physics2D.OverlapCircle(transform.position, 0.5f, blockLayer);

            if (detectedBlock != null)
            {
                Debug.Log("Block grabbed: " + detectedBlock.name);
                isGrabbing = true;
                blockCollider = detectedBlock;
            }
            else
            {
                Debug.Log("No block detected."); 
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isGrabbing = false;
            blockCollider = null;
        }
    }

    private void HandleMovement()
    {
        if (isGrabbing && blockCollider != null)
        {
            float moveInput = Input.GetAxisRaw("Horizontal");

            if (moveInput > 0) // Right
            {
                isPushing = true;
                isPulling = false;
                blockCollider.transform.position += new Vector3(pushPullSpeed * Time.deltaTime, 0, 0);
            }
            else if (moveInput < 0) // Left
            {
                isPulling = true;
                isPushing = false;
                blockCollider.transform.position -= new Vector3(pushPullSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                isPushing = false;
                isPulling = false;
            }

            rb.velocity = new Vector2(moveInput * pushPullSpeed, rb.velocity.y);
            PlayerVar.isPushing = isPushing;
            PlayerVar.isPulling = isPulling;
        }
        else
        {
            PlayerVar.isPushing = false;
            PlayerVar.isPulling = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
