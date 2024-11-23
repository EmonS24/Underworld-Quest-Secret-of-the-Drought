using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Transform currentCheckpoint;
    private PlayerHealth health;
    private PlayerVar player;

    private void Awake()
    {
        health = GetComponent<PlayerHealth>();
        player = GetComponent<PlayerVar>();
    }

    public void CheckRespawn()
    {
        transform.position = currentCheckpoint.position;
        health.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            Light checkpointLight = collision.GetComponentInChildren<Light>(); // Asumsikan ada child light pada checkpoint
            if (checkpointLight != null)
            {
                checkpointLight.enabled = true;
            }
        }
    }
}
