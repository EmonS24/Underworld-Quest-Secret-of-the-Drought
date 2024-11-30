using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerTrap : MonoBehaviour
{
    public float moveDistance = 2.0f; 
    public float moveSpeed = 1.0f;     
    private Vector3 initialPosition;    
    [SerializeField] private bool isOpen = false;      

    void Start()
    {
        initialPosition = transform.position; 
    }

    void Update()
    {
        if (isOpen)
        {
            if (transform.position.x > initialPosition.x - moveDistance)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.x < initialPosition.x)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
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
}
