using System.Collections;
using UnityEngine;

public class FlyDamage : MonoBehaviour
{
    public float damage;
    public PlayerHealth playerHp;
    public PlayerMov playerMov;
    private FlyFollow enemy;

    private bool canDamage = true;
    public float damageCooldown;  

    void Start()
    {
        enemy = GetComponent<FlyFollow>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canDamage)
        {
            enemy.isChasing = false;
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

            enemy.StartChaseCooldown();
            StartCoroutine(DamageCooldown());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.isChasing = true;
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;  
        yield return new WaitForSeconds(damageCooldown);  
        canDamage = true; 
    }
}
