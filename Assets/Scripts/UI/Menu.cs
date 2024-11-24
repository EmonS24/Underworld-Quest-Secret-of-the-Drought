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

    public void PlayGame()
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
