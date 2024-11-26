using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (!(PlayerPrefs.HasKey("CheckpointX") && PlayerPrefs.HasKey("CheckpointY")))
        {
            Debug.Log("No checkpoint found. Continue disabled.");
        }
    }

    public void PlayGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        PlayerPrefs.DeleteKey("CheckpointX");
        PlayerPrefs.DeleteKey("CheckpointY");
        SceneManager.LoadSceneAsync(1);
    }

        public void ContinueGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadSceneAsync(1);
    }

    public void Setting()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
    }

    public void MenuGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Debug.Log(message: "Quit!");
        Application.Quit();
    }
}
