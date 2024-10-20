using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMov playerMov;
    private Animator animator;
    public Transform ceilingCheck;
    public float ceilingCheckDistance = 0.2f;
    public LayerMask ceilingLayer;

    private Vector3 originalCeilingCheckPosition;
    private Vector3 crouchCeilingCheckPosition;

    public bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMov = GetComponent<PlayerMov>();
        animator = GetComponent<Animator>();

        originalCeilingCheckPosition = ceilingCheck.localPosition;
        crouchCeilingCheckPosition = new Vector3(originalCeilingCheckPosition.x, originalCeilingCheckPosition.y - 1.0f, originalCeilingCheckPosition.z);
    }

    void Update()
    {
        HandleCrouch();
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                StandUp();
            }
            else
            {
                Crouch();
            }
        }
    }

    private void Crouch()
    {
        if (!IsCeilingAbove())
        {
            ceilingCheck.localPosition = crouchCeilingCheckPosition;
            isCrouching = true;

            playerMov.SetCrouching(true);
        }
    }

    private void StandUp()
    {
        if (!IsCeilingAbove())
        {
            ceilingCheck.localPosition = originalCeilingCheckPosition;
            isCrouching = false;

            playerMov.SetCrouching(false);
        }
    }

    public bool IsCeilingAbove()
    {
        return Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, ceilingLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * ceilingCheckDistance);
        Gizmos.DrawSphere(ceilingCheck.position + Vector3.up * ceilingCheckDistance, 0.1f);
    }
}
