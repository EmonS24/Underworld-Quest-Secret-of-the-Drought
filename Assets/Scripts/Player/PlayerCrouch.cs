using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1f, 1);
    public Transform ceilingCheck; // Transform for checking the ceiling
    public float ceilingCheckDistance = 0.2f; // Distance for checking if there's a ceiling

    private bool isCrouching = false; // Track if the player is currently crouching

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCrouch();
    }

    private void HandleCrouch()
    {
        // Check if the player can stand up
        if (Input.GetKeyUp(KeyCode.LeftControl) && !IsCeilingAbove())
        {
            StandUp();
        }
        // Check if the player can crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private bool IsCeilingAbove()
    {
        // Cast a ray upwards to check if there is a ceiling above the player
        return Physics2D.Raycast(ceilingCheck.position, Vector2.up, ceilingCheckDistance);
    }

    private void Crouch()
    {
        // Set the player scale to crouching
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        isCrouching = true;
    }

    private void StandUp()
    {
        // Only stand up if the player is crouching
        if (isCrouching)
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            isCrouching = false;
        }
    }
}
