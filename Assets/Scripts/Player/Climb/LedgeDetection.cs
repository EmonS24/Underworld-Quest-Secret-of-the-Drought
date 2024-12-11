using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask pushableLayer;  

    [SerializeField] private PlayerClimb climb;
    [SerializeField] private PlayerVar player;
    public bool canDetected;

    private void Update()
    {
        if (player.isCrouching)
        {
            canDetected = false;
        }

        if (canDetected)
            climb.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, groundLayer | pushableLayer);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Pushable"))
            canDetected = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Pushable"))
            canDetected = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
