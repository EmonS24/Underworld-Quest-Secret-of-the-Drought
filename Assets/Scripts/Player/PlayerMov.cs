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
    private PlayerVar player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerVar>();
    }

    private void Update()
    {
        if (allowMove)
        {
            Move();
        }
    }

    private void Move()
    {
        moveInputH = Input.GetAxisRaw("Horizontal");

        float speed = GetCurrentSpeed();
        
        rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);

        FlipSprite();

        player.isMove = moveInputH != 0;
    }

    
    public float GetCurrentSpeed()
    {
        float speed;
        if (player.isCrouching)
        {
            speed = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
        return speed;
    }

    private void FlipSprite()
    {
        if (moveInputH > 0)
        {
            transform.localScale = new Vector2(1f, transform.localScale.y);
        }
        else if (moveInputH < 0)
        {
            transform.localScale = new Vector2(-1f, transform.localScale.y);
        }
    }
}
