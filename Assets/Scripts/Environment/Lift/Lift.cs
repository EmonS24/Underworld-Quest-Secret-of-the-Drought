using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public float moveDistance;  
    public float moveSpeed;    
    private Vector3 initialPosition;   
    private bool movingUp = true;      
    private List<Rigidbody2D> playersOnLift = new List<Rigidbody2D>(); 
    void Start()
    {
        initialPosition = transform.position;  
    }

    void FixedUpdate()
    {
        float targetY = movingUp ? initialPosition.y + moveDistance : initialPosition.y;
        
        float newY = Mathf.Lerp(transform.position.y, targetY, moveSpeed * Time.fixedDeltaTime);
        Vector3 previousPosition = transform.position;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        Vector3 liftDelta = transform.position - previousPosition;

        foreach (Rigidbody2D player in playersOnLift)
        {
            player.transform.position += liftDelta;
        }

        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            movingUp = !movingUp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playersOnLift.Add(playerRb); 
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playersOnLift.Remove(playerRb); 
            }
        }
    }
}
