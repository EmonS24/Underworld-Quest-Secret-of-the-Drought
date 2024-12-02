using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafhopperDamage : MonoBehaviour
{
    public float damage;
    public PlayerHealth playerHp;
    public PlayerMov playerMov;
    private LeafhopperVar Leafhopper;
    private LeafhopperMov LeafhopperMov;
    private PlayerVar player;

    public Collider2D attackTrigger;
    public Collider2D damageTrigger;

    private bool canDamage = true;  
    public float damageCooldown;  
    void Start()
    {
        player = FindObjectOfType<PlayerVar>();
        Leafhopper = GetComponent<LeafhopperVar>();
        LeafhopperMov = GetComponent<LeafhopperMov>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerVar>() != null && !player.isDeath && canDamage)
        {
            Leafhopper.isAttack = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerVar>() != null && damageTrigger.IsTouching(collision) && !player.isDeath)
        {
            playerHp.TakeDamage(damage);
            playerMov.KBCounter = playerMov.KBCTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMov.KnockFromRight = true;
            }
            else
            {
                playerMov.KnockFromRight = false;
            }

            LeafhopperMov.StartChaseCooldown();
            StartCoroutine(DamageCooldown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerVar>() != null)
        {
            Leafhopper.isAttack = false;
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);  
        canDamage = true;  
    }
}
