using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveDistance = 2.0f;
    public float moveSpeed = 1.0f;
    public bool moveDirectionUp = true;
    public bool moveDirectionHorizontal = false;
    public bool moveDirectionRight = true;
    private Vector3 initialPosition;
    private bool isOpen = false;
    public LayerMask obstacleLayer;

    private AudioSource audioSource;
    public AudioClip moveSound; 

    void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool isMoving = false;

        if (isOpen)
        {
            if (moveDirectionHorizontal)
            {
                if (moveDirectionRight)
                {
                    if (!Physics.Raycast(transform.position, Vector3.right, moveDistance, obstacleLayer) &&
                        transform.position.x < initialPosition.x + moveDistance)
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
                else
                {
                    if (!Physics.Raycast(transform.position, Vector3.left, moveDistance, obstacleLayer) &&
                        transform.position.x > initialPosition.x - moveDistance)
                    {
                        transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
            }
            else
            {
                if (moveDirectionUp)
                {
                    if (!Physics.Raycast(transform.position, Vector3.up, moveDistance, obstacleLayer) &&
                        transform.position.y < initialPosition.y + moveDistance)
                    {
                        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
                else
                {
                    if (!Physics.Raycast(transform.position, Vector3.down, moveDistance, obstacleLayer) &&
                        transform.position.y > initialPosition.y - moveDistance)
                    {
                        transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
            }
        }
        else
        {
            if (moveDirectionHorizontal)
            {
                if (moveDirectionRight)
                {
                    if (transform.position.x > initialPosition.x)
                    {
                        transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
                else
                {
                    if (transform.position.x < initialPosition.x)
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
            }
            else
            {
                if (moveDirectionUp)
                {
                    if (transform.position.y > initialPosition.y)
                    {
                        transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
                else
                {
                    if (transform.position.y < initialPosition.y)
                    {
                        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                        isMoving = true;
                    }
                }
            }
        }
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(moveSound);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (moveDirectionHorizontal)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * moveDistance);
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * moveDistance);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * moveDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * moveDistance);
        }
    }
}
