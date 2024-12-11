using System.Collections;
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
    public Vector2 detectionSizePositive; 
    public Vector2 detectionSizeNegative; 
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
        Vector3 positiveOffset = new Vector3(detectionSizePositive.x / 2, 0, 0);
        Vector3 negativeOffset = new Vector3(-detectionSizeNegative.x / 2, 0, 0);

        bool playerInPositiveDetection = Physics2D.OverlapBox(detectionArea.position + positiveOffset, detectionSizePositive, 0, playerLayer);
        bool playerInNegativeDetection = Physics2D.OverlapBox(detectionArea.position + negativeOffset, detectionSizeNegative, 0, playerLayer);

        bool playerInDetection = playerInPositiveDetection || playerInNegativeDetection;

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
        Vector3 positiveOffset = new Vector3(detectionSizePositive.x / 2, 0, 0);
        Vector3 negativeOffset = new Vector3(-detectionSizeNegative.x / 2, 0, 0);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(detectionArea.position + positiveOffset, detectionSizePositive);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(detectionArea.position + negativeOffset, detectionSizeNegative);
    }
}
