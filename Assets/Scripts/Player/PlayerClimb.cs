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
    private Vector2 climbBegunPosition; 
    private Vector2 climbOverPosition; 
    public LayerMask groundLayer;
    private bool isWallDetected;
    public bool ledgeDetected;
    private bool canGrabLedge = true;
    public bool canClimb;

    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
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

            climbBegunPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

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
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = climbOverPosition;

        canClimb = false;
        isClimbing = false; 
        animator.SetBool("canClimb", false); 
        rb.isKinematic = false; 

        AllowLedgeGrab();
    }

    private void AllowLedgeGrab() => canGrabLedge = true;

    public void CollisionCheck()
    {
        isWallDetected = Physics2D.BoxCast((Vector2)wallCheck.position + wallCheckOffset, wallCheckSize, 0f, Vector2.zero, 0f, groundLayer);

        Debug.Log($"Wall Detected: {isWallDetected}");
        Debug.Log($"Ledge Detected: {ledgeDetected}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube((Vector2)wallCheck.position + wallCheckOffset, wallCheckSize);
    }
}
