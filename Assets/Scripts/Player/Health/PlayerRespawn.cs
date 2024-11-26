using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerVar player;
    private PlayerHealth health;
    private Vector2 startPos;

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
        CheckpointData checkpointData = FindObjectOfType<CheckpointManager>().LoadCheckpoint();
        if (checkpointData != null)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
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
            Vector2 checkpointPosition = collision.transform.position;
            string sceneName = SceneManager.GetActiveScene().name;
            FindObjectOfType<CheckpointManager>().SaveCheckpoint(sceneName, checkpointPosition);
            Debug.Log("Checkpoint saved at: " + checkpointPosition);
        }
    }

    private void LoadCheckpoint()
    {
        CheckpointData checkpointData = FindObjectOfType<CheckpointManager>().LoadCheckpoint();

        if (checkpointData != null)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
        }
        else
        {
            transform.position = startPos;
        }
    }
}
