using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController door; 
    private int objectCount = 0;
    
    public AudioClip pressurePlateSound; 
    private AudioSource audioSource; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Pushable"))
        {
            objectCount++;
            door.OpenDoor();

            if (pressurePlateSound != null && audioSource != null) 
            {
                audioSource.PlayOneShot(pressurePlateSound); 
            }
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
