using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("Stamina Settings")]
    public Image staminaBar; 
    public float stamina, maxStamina;
    public float runCost;
    public float chargeRate;
    private Coroutine recharge;

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

        UpdateStamina();
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
        if (Input.GetKey(KeyCode.LeftShift) && moveInputH != 0 && stamina > 0)
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
