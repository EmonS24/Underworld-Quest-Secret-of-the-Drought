using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;
    public PlayerHealth playerHp;
    public PlayerMov playerMov;
    private EnemyVar enemy;

    void Start()
    {
        enemy = GetComponent<EnemyVar>();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.isAttack = true;
            enemy.isMove = false; 

            playerHp.TakeDamage(damage);
            playerMov.KBCounter = playerMov.KBCTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerMov.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMov.KnockFromRight = false;
            }
        }
        else
        {
            enemy.isAttack = false;
        }
    }

}
