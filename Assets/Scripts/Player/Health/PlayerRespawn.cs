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
        LoadCheckpoint();
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
        respawnPanel.SetActive(true);
    }

    private void CheckRespawn()
    {
        health.Respawn(); 
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
            PlayerPrefs.SetFloat("CheckpointX", currentCheckpoint.position.x);
            PlayerPrefs.SetFloat("CheckpointY", currentCheckpoint.position.y);

            Debug.Log("Checkpoint: " + currentCheckpoint.position);
        }
    }

    private void LoadCheckpoint()
    {
        if (PlayerPrefs.HasKey("CheckpointX") && PlayerPrefs.HasKey("CheckpointY"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");

            currentCheckpoint = new GameObject("LoadedCheckpoint").transform;
            currentCheckpoint.position = new Vector2(x, y);

            transform.position = currentCheckpoint.position;
        }
        else
        {
            Debug.Log("No checkpoint found, starting from default position.");
        }
    }
}