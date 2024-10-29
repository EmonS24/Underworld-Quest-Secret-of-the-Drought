using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private Transform wallCheck; 
    [SerializeField] private Vector2 wallCheckSize; 
    [SerializeField] private Vector2 wallCheckOffset; 
    [SerializeField] private Vector2 offset1; 
    [SerializeField] private Vector2 offset2; 
    [SerializeField] private float climbDuration;
    private Vector2 climbBegunPosition; 
    private Vector2 climbOverPosition; 
    public LayerMask groundLayer;
    private bool isWallDetected;
    public bool ledgeDetected;
    private bool canGrabLedge = true;
    public bool canClimb;
    private PlayerMov playerMov;

    private bool isClimbing = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        playerMov = GetComponent<PlayerMov>();
    }

    void Update()
    {
        CollisionCheck();

        if (!isClimbing)
        {
            CheckForLedge();
        }
    }

    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabLedge)
        {
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            if (transform.localScale.x < 0)
            {
                climbBegunPosition = ledgePosition + new Vector2(-offset1.x, offset1.y);
                climbOverPosition = ledgePosition + new Vector2(-offset2.x, offset2.y);
            }
            else 
            {
                climbBegunPosition = ledgePosition + offset1;
                climbOverPosition = ledgePosition + offset2;
            }

            canClimb = true;
            isClimbing = true; 
            animator.SetBool("canClimb", true); 

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            transform.position = climbBegunPosition; 

            StartCoroutine(ClimbCoroutine());
        }
    }

    private IEnumerator ClimbCoroutine()
    {
        playerMov.allowMove = false;

        yield return new WaitForSeconds(climbDuration);
        playerMov.allowMove = true;

        transform.position = climbOverPosition;

        // Reset climbing state
        canGrabLedge = true;
        canClimb = false;
        isClimbing = false;
        rb.isKinematic = false; 

        animator.SetBool("canClimb", false);
    }

    public void CollisionCheck()
    {
        isWallDetected = Physics2D.BoxCast((Vector2)wallCheck.position + wallCheckOffset, wallCheckSize, 0f, Vector2.zero, 0f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)wallCheck.position + wallCheckOffset, wallCheckSize);
    }
}
