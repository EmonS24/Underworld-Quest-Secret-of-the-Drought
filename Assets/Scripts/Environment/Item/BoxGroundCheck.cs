using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 boxGroundCheckSize = new Vector2(0.5f, 0.1f); 
    [SerializeField] private Vector2 boxGroundCheckOffset = new Vector2(0f, -0.5f);

    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapBox((Vector2)transform.position + boxGroundCheckOffset, boxGroundCheckSize, 0f, groundLayer);
        return groundCheck != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2)transform.position + boxGroundCheckOffset, boxGroundCheckSize);
    }
}
