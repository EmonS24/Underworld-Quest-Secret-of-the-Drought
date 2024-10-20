using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInputH;
    [SerializeField] public float walkSpeed;    // Speed when walking
    [SerializeField] public float runSpeed;    // Speed when running
    [SerializeField] public float crouchSpeed; // Speed when crouching
    public bool isCrouching = false; // Track if the player is crouching

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    public void SetCrouching(bool crouching)
    {
        isCrouching = crouching; 
    }

    public void Move()
    {
        moveInputH = Input.GetAxisRaw("Horizontal");

        float speed = isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);

        rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);

        // Flip sprite 
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
        return isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
    }

    public bool IsMoving()
    {
        return moveInputH != 0;
    }

    public bool IsFalling()
    {
        return rb.velocity.y < 0 && !PlayerVar.isGrounded;
    }
}