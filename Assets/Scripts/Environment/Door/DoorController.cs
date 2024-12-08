using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveDistance; 
    public float moveSpeed;     
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
            if (transform.position.y < initialPosition.y + moveDistance)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.y > initialPosition.y)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
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
