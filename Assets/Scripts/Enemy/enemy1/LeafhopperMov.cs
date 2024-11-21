using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafhopperMov : MonoBehaviour
{
    private LeafhopperVar Leafhopper;
    private PlayerVar player; 
    public Transform[] patrolPoints;
    public float chaseSpeed;
    public float moveSpeed;
    public int patrolDestination;
    public float chaseDistance;
    public float distanceToPlayer;
    public float stopChaseDistance;

    void Start()
    {
        Leafhopper = GetComponent<LeafhopperVar>();
        player = FindObjectOfType<PlayerVar>(); 
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (Leafhopper.isChasing)
        {
            if (distanceToPlayer > stopChaseDistance || player.isDeath)
            {
                Leafhopper.isChasing = false;
            }
            else
            {
                FlipSprite(player.transform.position.x);
                if (transform.position.x > player.transform.position.x)
                {
                    transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
                    Leafhopper.isMove = true;
                }
                else if (transform.position.x < player.transform.position.x)
                {
                    transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
                    Leafhopper.isMove = true;
                }
            }
        }
        else
        {
            if (distanceToPlayer < chaseDistance && !player.isDeath)
            {
                Leafhopper.isChasing = true;
            }

            Patrol();
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[patrolDestination];
        FlipSprite(target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;
        }
    }

    private void FlipSprite(float targetX)
    {
        if (targetX < transform.position.x)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z); 
        }
        else if (targetX > transform.position.x)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}
