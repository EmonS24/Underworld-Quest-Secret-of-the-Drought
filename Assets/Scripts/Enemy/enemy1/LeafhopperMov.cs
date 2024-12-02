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

    public Transform detectionArea;
    public Vector2 detectionSize;
    public LayerMask playerLayer;

    private bool isOnCooldown = false;  
    public float chaseCooldownTime;  

    void Start()
    {
        Leafhopper = GetComponent<LeafhopperVar>();
        player = FindObjectOfType<PlayerVar>();
    }

    void Update()
    {
        bool playerInDetection = Physics2D.OverlapBox(detectionArea.position, detectionSize, 0, playerLayer);

        if (Leafhopper.isChasing && !isOnCooldown)
        {
            if (!playerInDetection || player.isDeath)
            {
                Leafhopper.isChasing = false;
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            if (playerInDetection && !player.isDeath)
            {
                Leafhopper.isChasing = true;
            }
            else
            {
                Patrol();
            }
        }
    }

    private void ChasePlayer()
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectionArea.position, detectionSize);
    }
}
