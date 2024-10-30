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
    public LayerMask groundLayer;
    public bool ledgeDetected;
    private bool canGrabLedge = true;
    private PlayerVar player;
    private PlayerMov move;

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
        if (ledgeDetected && canGrabLedge && !player.isGrounded)
        {
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            climbBegunPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

            player.canClimb = true; 

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
        player.canClimb = false;
        rb.isKinematic = false; 
    }
}
