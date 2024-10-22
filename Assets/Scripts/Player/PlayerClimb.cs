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

    [SerializeField] private Vector2 offset1; // Offset to the starting position of the climb
    [SerializeField] private Vector2 offset2; // Offset to the end position of the climb
    private Vector2 climbBegunPosition; // Position where the climb begins
    private Vector2 climbOverPosition; // Position where the climb ends
    public LayerMask groundLayer;
    private bool isWallDetected;
    public bool ledgeDetected;
    private bool canGrabLedge = true;
    public bool canClimb;

    private bool isClimbing = false; // State to track climbing

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Animator reference
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

            // Adjust climb positions based on the player's facing direction
            if (transform.localScale.x < 0) // If the player is facing left
            {
                climbBegunPosition = ledgePosition + new Vector2(-offset1.x, offset1.y);
                climbOverPosition = ledgePosition + new Vector2(-offset2.x, offset2.y);
            }
            else // If the player is facing right
            {
                climbBegunPosition = ledgePosition + offset1;
                climbOverPosition = ledgePosition + offset2;
            }

            canClimb = true;
            isClimbing = true; // Start climbing
            animator.SetBool("canClimb", true); // Trigger climbing animation

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
        isClimbing = false; // Reset climbing state
        animator.SetBool("canClimb", false); // End climbing animation
        rb.isKinematic = false; // Re-enable player control

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
