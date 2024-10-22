using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
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


    void Update()
    {
        CollisionCheck();
        CheckForLedge();
        LedgeClimbOver();
        AllowLedgeGrab();
    }

    private void CheckForLedge()
    {
        if(ledgeDetected && canGrabLedge)
        {
            canGrabLedge = false;

            Vector2 ledgePosition = GetComponentInChildren<LedgeDetection>().transform.position;

            climbBegunPosition = ledgePosition + offset1;
            climbOverPosition = ledgePosition + offset2;

            canClimb = true;
        }
    }

private void LedgeClimbOver()
{
    if (canClimb) // Make sure this is the right condition
    {
        transform.position = climbOverPosition;
        canClimb = false; // This might be causing issues
        Invoke("AllowLedgeGrab", 0.1f);
    }
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
