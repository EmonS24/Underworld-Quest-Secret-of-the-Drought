using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 startPos;
    private Transform currentCheckpoint;
    private PlayerVar player;
    private PlayerHealth health;

    public GameObject respawnPanel; 
    public float respawnDelay; 

    private void Start()
    {
        player = GetComponent<PlayerVar>();
        health = GetComponent<PlayerHealth>();
        startPos = transform.position;
        respawnPanel.SetActive(false);
    }

    private void Update()
    {
        if (player.isDeath && !respawnPanel.activeSelf)
        {
            StartCoroutine(HandleDeathSequence()); 
        }

        if (respawnPanel.activeSelf && Input.anyKeyDown)
        {
            CheckRespawn(); 
        }
    }

    private IEnumerator HandleDeathSequence()
    {
        yield return new WaitForSeconds(respawnDelay); 
        ShowRespawnPanel(); 
    }

    private void ShowRespawnPanel()
    {
        respawnPanel.SetActive(true);
    }

    private void CheckRespawn()
    {
        health.Respawn(); 
        // Tentukan posisi respawn
        if (currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
        }
        else
        {
            transform.position = startPos;
        }

        respawnPanel.SetActive(false); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform; 
            collision.GetComponent<Collider2D>().enabled = false;

            Debug.Log("Checkpoint: " + currentCheckpoint.position);
        }
    }
}