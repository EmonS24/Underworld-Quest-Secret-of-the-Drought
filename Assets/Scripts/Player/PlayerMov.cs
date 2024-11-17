using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMov : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveInputH;
    [SerializeField] public float walkSpeed;    
    [SerializeField] public float runSpeed;    
    [SerializeField] public float crouchSpeed; 
    [SerializeField] public float grabSpeed;
    public bool allowMove = true;
    private PlayerVar player;
    public float KBForce;
    public float KBCounter;
    public float KBCTotalTime;

    public bool KnockFromRight;

    [Header("Stamina Settings")]
    public Image staminaBar; 
    public float stamina, maxStamina;
    public float runCost;
    public float chargeRate;
    private Coroutine recharge;

    private AudioManager audioManager;

    private float stepTimer;
    private float stepInterval = 0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerVar>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        if (allowMove)
        {
            Move();
        }

        UpdateStamina();
    }

    private void Move()
    {
        moveInputH = Input.GetAxisRaw("Horizontal");

        float speed = GetCurrentSpeed();

        if (speed == runSpeed)
        {
            stepInterval = 0.3f;
        }
        else if (speed == walkSpeed)
        {
            stepInterval = 0.4f;
        }
        else
        {
            stepInterval = 0.7f;
        }

        FlipSprite();

        player.isMove = moveInputH != 0;

        if (player.isMove && audioManager != null && !audioManager.SFXSource.isPlaying && player.isGrounded)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                audioManager.PlaySFX(audioManager.footsteps);
                stepTimer = stepInterval;
            }
        }

        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);
        }
        else
        {
            if (KnockFromRight)
            {
                rb.velocity = new Vector2(-KBForce, rb.velocity.y);
            }
            if (!KnockFromRight)
            {
                rb.velocity = new Vector2(KBForce, rb.velocity.y);
            }

            KBCounter -= Time.deltaTime;
        }
    }

    public float GetCurrentSpeed()
    {
        if (player.isGrabbing)
        {
            return grabSpeed;
        }
        else if (player.isCrouching)
        {
            return crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            return runSpeed;
        }
        return walkSpeed;
    }

    private void UpdateStamina()
    {
        if (Input.GetKey(KeyCode.LeftShift) && moveInputH != 0 && stamina > 0 && !player.isCrouching && !player.isGrabbing && player.isGrounded)
        {
            stamina -= runCost * Time.deltaTime;
            if(stamina < 0) stamina = 0;
            staminaBar.fillAmount = stamina / maxStamina;

            if(recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
    }

    private void FlipSprite()
    {
        if (!player.isGrabbing)
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

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if(stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }
}
