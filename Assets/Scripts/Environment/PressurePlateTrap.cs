using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTrap : MonoBehaviour
{
    public DoorControllerTrap door; 

    private int objectCount = 0; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable"))
        {
            objectCount++; 
            door.OpenDoor(); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable"))
        {
            objectCount--; 
            if (objectCount <= 0)
            {
                door.CloseDoor(); 
            }
        }
    }
}
