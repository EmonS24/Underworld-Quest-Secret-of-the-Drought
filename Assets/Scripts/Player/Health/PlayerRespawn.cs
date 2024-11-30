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

    [SerializeField] private QuestLogManager questLogManager;
    [SerializeField] private CheckpointManager checkpointManager;

    private void Start()
    {
        player = GetComponent<PlayerVar>();
        health = GetComponent<PlayerHealth>();
        startPos = transform.position;

        respawnPanel.SetActive(false);
        checkpointManager = FindObjectOfType<CheckpointManager>();
        questLogManager = FindObjectOfType<QuestLogManager>(); 

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

        CheckpointData checkpointData = checkpointManager.LoadCheckpoint();
        if (checkpointData != null)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);

            // Update quest progress and item collection
            questLogManager.LoadQuestProgress(checkpointData.questProgress);
            checkpointManager.LoadCollectedItems(checkpointData.collectedItems);
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
            int currentQuestProgress = questLogManager.GetQuestProgress(); 
            checkpointManager.SaveCheckpoint(sceneName, checkpointPosition, currentQuestProgress);

            Debug.Log("Checkpoint saved at: " + checkpointPosition);
        }
    }

    private void LoadCheckpoint()
    {
        CheckpointData checkpointData = checkpointManager.LoadCheckpoint();
        
        if (checkpointData != null && checkpointData.sceneName == SceneManager.GetActiveScene().name)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
            questLogManager.LoadQuestProgress(checkpointData.questProgress); 
            checkpointManager.LoadCollectedItems(checkpointData.collectedItems);
            Debug.Log("Loaded checkpoint for scene: " + checkpointData.sceneName);
        }
        else
        {
            transform.position = startPos; 
            Debug.Log("No valid checkpoint found, using start position.");
        }
    }
}
