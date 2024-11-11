using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    private EnemyVar enemy;
    public Transform[] patrolPoints;
    public float chaseSpeed;
    public float moveSpeed;
    public int patrolDestination;
    public Transform playerTransform;
    public float chaseDistance;
    public float distanceToPlayer;
    public float stopChaseDistance;
    private PlayerVar player;

    void Start()
    {
        enemy = GetComponent<EnemyVar>();
        player = playerTransform.GetComponent<PlayerVar>();
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (player.isDeath)
        {
            enemy.isChasing = false;
            enemy.isAttack = false; 
        }

        if (playerTransform.position.y > transform.position.y && player.isGrounded)
        {
            enemy.isChasing = false; 
            enemy.isAttack = false;
        }

        if (enemy.isChasing)
        {
            if (distanceToPlayer > stopChaseDistance)
            {
                enemy.isChasing = false;
                enemy.isAttack = false;
            }
            else
            {
                if (transform.position.x > playerTransform.position.x)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                    transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
                    enemy.isMove = true;
                }
                else if (transform.position.x < playerTransform.position.x)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
                    enemy.isMove = true;
                }
            }
        }
        else
        {
            if (!enemy.isChasing)
            {
                if (transform.position.x > patrolPoints[patrolDestination].position.x)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y); 
                }
                else
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y); 
                }
            }
            if (distanceToPlayer < chaseDistance)
            {
                enemy.isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                    patrolDestination = 1;
                    enemy.isMove = false;
                }
                else
                {
                    enemy.isMove = true;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                    patrolDestination = 0;
                    enemy.isMove = false;
                }
                else
                {
                    enemy.isMove = true;
                }
            }
        }
    }
}
