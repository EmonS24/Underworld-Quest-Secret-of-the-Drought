using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    public Transform ceilingCheck;
    public float ceilingCheckDistance = 0.2f;
    public LayerMask ceilingLayer;

    private Vector3 originalCeilingCheckPosition;
    private Vector3 crouchCeilingCheckPosition;
    private PlayerVar player;

    void Start()
    {
        player = GetComponent<PlayerVar>();

        originalCeilingCheckPosition = ceilingCheck.localPosition;
        crouchCeilingCheckPosition = new Vector2(originalCeilingCheckPosition.x, originalCeilingCheckPosition.y - 1.0f);
    }

    void Update()
    {
        HandleCrouch();
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (player.isCrouching)
            {
                StandUp();
            }
            else
            {
                Crouch();
            }
        }
    }

    public void SetCrouching(bool crouching)
    {
        player.isCrouching = crouching; 
    }

    private void Crouch()
    {
        if (!IsCeilingAbove())
        {
            ceilingCheck.localPosition = crouchCeilingCheckPosition;
            player.isCrouching = true;

            SetCrouching(true);
        }
    }

    private void StandUp()
    {
        if (!IsCeilingAbove())
        {
            ceilingCheck.localPosition = originalCeilingCheckPosition;
            player.isCrouching = false;

            SetCrouching(false);
        }
    }

    public bool IsCeilingAbove()
    {
        return Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance, ceilingLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ceilingCheck.position, ceilingCheck.position + Vector3.up * ceilingCheckDistance);
        Gizmos.DrawSphere(ceilingCheck.position + Vector3.up * ceilingCheckDistance, 0.1f);
    }
}
