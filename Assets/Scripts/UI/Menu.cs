using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class Menu : MonoBehaviour
{
    AudioManager audioManager;
    public string nextSceneName;

    public GameObject loadingScreen;
    public Slider loadingSlider;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (FindObjectOfType<CheckpointManager>().LoadCheckpoint() == null)
        {
            Debug.Log("No checkpoint found. Continue disabled.");
        }
    }

    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        FindObjectOfType<CheckpointManager>().ClearCheckpoint();
        SceneManager.LoadSceneAsync(nextSceneName);
    }

    public void ContinueGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);

        CheckpointData checkpointData = FindObjectOfType<CheckpointManager>().LoadCheckpoint();

        if (checkpointData != null)
        {
            // Tampilkan loading screen
            loadingScreen.SetActive(true);

            // Mulai memuat scene secara asynchronous
            StartCoroutine(LoadSceneAsync(checkpointData.sceneName, checkpointData));
        }
        else
        {
            Debug.Log("No checkpoint found, unable to continue.");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName, CheckpointData checkpointData)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            if (loadingSlider != null)
            {
                loadingSlider.value = operation.progress;
            }
            yield return null;
        }

        StartCoroutine(LoadPlayerAtCheckpoint(checkpointData));

        loadingScreen.SetActive(false);
    }

    private IEnumerator LoadPlayerAtCheckpoint(CheckpointData checkpointData)
    {
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == checkpointData.sceneName);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector2(checkpointData.posX, checkpointData.posY);
            Debug.Log("Player position set to checkpoint: " + player.transform.position);
        }
    }

    public void Setting()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void MenuGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Debug.Log("Quit!");
        Application.Quit();
    }
}
