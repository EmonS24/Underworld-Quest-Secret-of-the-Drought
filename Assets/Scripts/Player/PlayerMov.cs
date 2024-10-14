using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInputH;
    [SerializeField] private float walkSpeed;    // Adjustable in Unity
    [SerializeField] private float runSpeed;    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Horizontal movement input
        moveInputH = Input.GetAxisRaw("Horizontal");

        // Determine the speed based on whether the player is running
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Set the player's horizontal velocity
        rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);

        // Flip the player sprite based on movement direction
        if (moveInputH > 0)
        {
            transform.localScale = new Vector2(1f, transform.localScale.y);  // Facing right
        }
        else if (moveInputH < 0)
        {
            transform.localScale = new Vector2(-1f, transform.localScale.y);  // Facing left
        }

        // Update movement state (if needed)
        PlayerVar.isMove = moveInputH != 0;
    }
}
