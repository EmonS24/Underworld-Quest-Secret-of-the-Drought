using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMov : MonoBehaviour
{
    private RatVar enemy;
    private PlayerVar player; 
    public Transform[] patrolPoints;
    public float chaseSpeed;
    public float moveSpeed;
    public int patrolDestination;
    public float chaseDistance;
    public float stopChaseDistance;

    void Start()
    {
        enemy = GetComponent<RatVar>();
        player = FindObjectOfType<PlayerVar>(); 
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (player.isDeath)
        {
            enemy.isChasing = false;
            enemy.isAttack = false;
        }

        if (player.transform.position.y > transform.position.y && player.isGrounded)
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
                ChasePlayer(distanceToPlayer);
            }
        }
        else
        {
            Patrol(distanceToPlayer);
        }
    }

    private void ChasePlayer(float distanceToPlayer)
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
            enemy.isMove = true;
        }
        else if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
            enemy.isMove = true;
        }
    }

    private void Patrol(float distanceToPlayer)
    {
        if (distanceToPlayer < chaseDistance)
        {
            enemy.isChasing = true;
        }

        Transform targetPoint = patrolPoints[patrolDestination];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);
        enemy.isMove = Vector2.Distance(transform.position, targetPoint.position) >= 0.2f;

        if (!enemy.isMove)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
            transform.localScale = new Vector2(transform.position.x > patrolPoints[patrolDestination].position.x ? 1 : -1, transform.localScale.y);
        }
    }
}
