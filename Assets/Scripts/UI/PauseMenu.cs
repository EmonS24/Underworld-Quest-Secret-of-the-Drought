using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Pause()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Continue()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;   
    }

    public void QuitGame()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Debug.Log("Quit!");
        Application.Quit();
    }
}
