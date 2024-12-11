using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Vector2 boxSize = new Vector2(0.5f, 0.1f);
    private PlayerVar player;

    private AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerVar>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        CheckGrounded();
        HandleJump();
    }

    public void HandleJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space) && !player.isCrouching && !player.isGrabbing && !player.isClimbing)
        {
            player.isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.jumpStep);
            }
        }
    }

    public void CheckGrounded()
    {
        bool isGroundedNow = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);

        if (isGroundedNow && !player.isGrounded && audioManager != null)
        {
            audioManager.PlaySFX(audioManager.jumpGround);
        }

        player.isGrounded = isGroundedNow;

        if (player.isGrounded)
        {
            player.isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
}
