using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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

    private AudioManager audioManager;
    public AudioClip checkpointSound; 

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

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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

    private void CheckRespawn()
    {
        health.Respawn();

        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);

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
            Animator checkpointAnimator = collision.GetComponent<Animator>();
            if (checkpointAnimator != null)
                checkpointAnimator.SetTrigger("Activate");

            Light2D checkpointLight = collision.GetComponentInChildren<Light2D>();
            if (checkpointLight != null)
                StartCoroutine(FadeInLight(checkpointLight));

            if (checkpointSound != null)
            {
                audioManager.PlaySFX(checkpointSound);
            }

            Vector2 checkpointPosition = collision.transform.position;
            string sceneName = SceneManager.GetActiveScene().name;
            int currentQuestProgress = questLogManager.GetQuestProgress();
            checkpointManager.SaveCheckpoint(sceneName, checkpointPosition, currentQuestProgress, health.health);
            health.AddHealth(health.maxHealth);

            Collider2D checkpointCollider = collision.GetComponent<Collider2D>();
            if (checkpointCollider != null)
            {
                checkpointCollider.enabled = false;
            }
        }
    }

    private IEnumerator FadeInLight(Light2D light)
    {
        float timeElapsed = 0f;
        float fadeDuration = 1f;
        float startIntensity = 0f;
        float targetIntensity = light.intensity;

        light.intensity = startIntensity;
        light.enabled = true;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, timeElapsed / fadeDuration);
            yield return null;
        }

        light.intensity = targetIntensity;
    }

    private void LoadCheckpoint()
    {
        CheckpointData checkpointData = checkpointManager.LoadCheckpoint();

        if (checkpointData != null && checkpointData.sceneName == SceneManager.GetActiveScene().name)
        {
            transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
            questLogManager.LoadQuestProgress(checkpointData.questProgress);
            checkpointManager.LoadCollectedItems(checkpointData.collectedItems);
        }
        else
        {
            transform.position = startPos;
        }
    }
}
