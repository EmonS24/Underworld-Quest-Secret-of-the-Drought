using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInputH;
    [SerializeField] public float walkSpeed;    // Speed when walking
    [SerializeField] public float runSpeed;    // Speed when running
    [SerializeField] public float crouchSpeed; // Speed when crouching
    public bool allowMove = true;
    
    // New variable to track the last action
    private bool lastActionWasPulling = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (allowMove)
        {
            Move();
        }
    }

    public void SetCrouching(bool crouching)
    {
        PlayerVar.isCrouching = crouching; 
    }

    public void Move()
    {
        moveInputH = Input.GetAxisRaw("Horizontal");

        float speed = PlayerVar.isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);

        rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);

        // Check for grabbing and update lastActionWasPulling
        if (PlayerVar.isGrabbing)
        {
            // Do not flip the sprite if the last action was pulling
            if (lastActionWasPulling)
            {
                return;
            }
        }

        // Flip sprite based on movement input
        if (moveInputH > 0)
        {
            transform.localScale = new Vector2(1f, transform.localScale.y);  
        }
        else if (moveInputH < 0)
        {
            transform.localScale = new Vector2(-1f, transform.localScale.y); 
        }
        
        PlayerVar.isMove = moveInputH != 0;
    }

    public float GetCurrentSpeed()
    {
        return PlayerVar.isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
    }

    public bool IsMoving()
    {
        return moveInputH != 0;
    }

    public bool IsFalling()
    {
        return rb.velocity.y < 0 && !PlayerVar.isGrounded;
    }

    // Call this method when the player starts pulling
    public void SetLastActionToPulling(bool isPulling)
    {
        lastActionWasPulling = isPulling;
    }
}
