using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  

public class SkipCutscene : MonoBehaviour
{
    AudioManager audioManager;
    public string nextSceneName = "GameScene";  

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnSkipClicked()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadSceneAsync(nextSceneName);
    }
}
