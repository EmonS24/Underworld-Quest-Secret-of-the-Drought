using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour
{
    AudioManager audioManager;
    public string nextSceneName;

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
            SceneManager.LoadSceneAsync(checkpointData.sceneName);
            StartCoroutine(LoadPlayerAtCheckpoint(checkpointData));
        }
        else
        {
            Debug.Log("No checkpoint found, unable to continue.");
        }
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
