using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    private PlayerVar player;
    private PlayerHealth health;
    private Vector2 startPos;

    public GameObject respawnPanel;
    public float respawnDelay;
    private CanvasGroup respawnCanvasGroup;

    [SerializeField] private QuestLogManager questLogManager;
    [SerializeField] private CheckpointManager checkpointManager;

    private void Start()
    {
        player = GetComponent<PlayerVar>();
        health = GetComponent<PlayerHealth>();
        startPos = transform.position;

        respawnPanel.SetActive(false);
        respawnCanvasGroup = respawnPanel.GetComponent<CanvasGroup>(); 
        respawnCanvasGroup.alpha = 0f;  

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
        StartCoroutine(FadeInRespawnPanel());  
    }

    private IEnumerator FadeInRespawnPanel()
    {
        float timeElapsed = 0f;
        float fadeDuration = 1f;  

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            respawnCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);  
            yield return null;
        }

        respawnCanvasGroup.alpha = 1f;  
    }

    private void CheckRespawn()
    {
        health.Respawn();

        CheckpointData checkpointData = checkpointManager.LoadCheckpoint();
        if (checkpointData != null)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
            questLogManager.LoadQuestProgress(checkpointData.questProgress);
            checkpointManager.LoadCollectedItems(checkpointData.collectedItems);
        }
        else
        {
            transform.position = startPos;
        }

        StartCoroutine(FadeOutRespawnPanel());  
    }

    private IEnumerator FadeOutRespawnPanel()
    {
        float timeElapsed = 0f;
        float fadeDuration = 1f;  

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            respawnCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            yield return null;
        }

        respawnCanvasGroup.alpha = 0f;  
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
