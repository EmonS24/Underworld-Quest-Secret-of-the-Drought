using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFollow : MonoBehaviour
{
    public float speed;
    public float chaseSpeed;
    public float lineOfSite;
    public Transform[] patrolPoints;

    private int patrolIndex = 0; 
    private float patrolWaitCounter;
    private PlayerVar player; 
    public bool isChasing = false;

    private bool isOnCooldown = false;  
    public float chaseCooldownTime;  

    void Start()
    {
        player = FindObjectOfType<PlayerVar>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (isOnCooldown)
        {
            isChasing = false;
            return;
        }

        if (distanceFromPlayer < lineOfSite && !player.isDeath)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void ChasePlayer()
    {
        FlipSprite(player.transform.position.x);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chaseSpeed * Time.deltaTime);
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[patrolIndex];
        FlipSprite(targetPoint.position.x);
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            patrolWaitCounter -= Time.deltaTime;

            if (patrolWaitCounter <= 0)
            {
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
            }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);

        Gizmos.color = Color.blue;
        foreach (Transform point in patrolPoints)
        {
            Gizmos.DrawWireSphere(point.position, 0.2f);
        }
    }

    public void StartChaseCooldown()
    {
        if (!isOnCooldown)
        {
            isOnCooldown = true;
            StartCoroutine(ChaseCooldown());
        }
    }

    private IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(chaseCooldownTime);
        isOnCooldown = false;
    }
}
