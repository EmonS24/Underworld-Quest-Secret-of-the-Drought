using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector2 offset1; 
    [SerializeField] private Vector2 offset2; 
    [SerializeField] private float climbDuration;
    private Vector2 climbBegunPosition; 
    private Vector2 climbOverPosition; 
    public bool ledgeDetected;
    private bool canGrabLedge = true;
    private PlayerVar player;
    private PlayerMov move;
    private bool isFacingRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerVar>();
        move = GetComponent<PlayerMov>();
    }

    void Update()
    {
        CheckForLedge();
    }

    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabLedge && !player.isGrounded && !player.isGrabbing)
        {
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            isFacingRight = transform.localScale.x > 0;

            if (isFacingRight)
            {
                climbBegunPosition = ledgePosition + offset1;
                climbOverPosition = ledgePosition + offset2;
            }
            else
            {
                climbBegunPosition = ledgePosition + new Vector2(-offset1.x, offset1.y);
                climbOverPosition = ledgePosition + new Vector2(-offset2.x, offset2.y);
            }

            player.isClimbing = true; 

            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

            transform.position = climbBegunPosition; 

            StartCoroutine(ClimbCoroutine());
        }
    }

    private IEnumerator ClimbCoroutine()
    {
        move.allowMove = false;

        yield return new WaitForSeconds(climbDuration);
        move.allowMove = true;

        transform.position = climbOverPosition;

        canGrabLedge = true;
        player.isClimbing = false;
        rb.isKinematic = false; 
    }
}
