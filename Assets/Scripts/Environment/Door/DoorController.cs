using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
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
                    }
                }
                else
                {
                    // Raycast ke kiri
                    if (!Physics.Raycast(transform.position, Vector3.left, moveDistance, obstacleLayer) &&
                        transform.position.x > initialPosition.x - moveDistance)
                    {
                        transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
                    }
                }
            }
            else
            {
                if (moveDirectionUp)
                {
                    // Raycast ke atas
                    if (!Physics.Raycast(transform.position, Vector3.up, moveDistance, obstacleLayer) &&
                        transform.position.y < initialPosition.y + moveDistance)
                    {
                        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    // Raycast ke bawah
                    if (!Physics.Raycast(transform.position, Vector3.down, moveDistance, obstacleLayer) &&
                        transform.position.y > initialPosition.y - moveDistance)
                    {
                        transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
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
                    }
                }
                else
                {
                    if (transform.position.x < initialPosition.x)
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
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
                    }
                }
                else
                {
                    if (transform.position.y < initialPosition.y)
                    {
                        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                    }
                }
            }
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