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
    [SerializeField] public float grabSpeed; // Kecepatan khusus saat grabbing
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
        if (player.isGrabbing) // Cek jika player sedang grabbing
        {
            return grabSpeed; // Kembalikan kecepatan grabbing
        }
        else if (player.isCrouching)
        {
            return crouchSpeed; // Ambil kecepatan crouch dari PlayerVar
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            return runSpeed; // Ambil kecepatan run dari PlayerVar
        }
        return walkSpeed; // Ambil kecepatan walk dari PlayerVar
    }

    private void FlipSprite()
    {
        if(!player.isGrabbing)
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
}
