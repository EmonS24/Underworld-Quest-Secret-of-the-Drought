using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyDamage : MonoBehaviour
{
    public float damage;
    public PlayerHealth playerHp;
    public PlayerMov playerMov;
    private FlyFollow enemy;

    void Start()
    {
        enemy = GetComponent<FlyFollow>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.isChasing = true;
        }
    }
}
