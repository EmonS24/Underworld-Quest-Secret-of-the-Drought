using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInputH;
    [SerializeField] public float walkSpeed;    
    [SerializeField] public float runSpeed;    
    [SerializeField] public float crouchSpeed; 
    public bool allowMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (allowMove)
        {
            Move();
        }
    }

    public void Move()
    {
        moveInputH = Input.GetAxisRaw("Horizontal");

        float speed = PlayerVar.isCrouching ? crouchSpeed : (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
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

    public void SetCrouching(bool crouching)
    {
        PlayerVar.isCrouching = crouching; 
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
}