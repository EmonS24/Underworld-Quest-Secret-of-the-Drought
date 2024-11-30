using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipCutscene : MonoBehaviour
{
    AudioManager audioManager;
    public string nextSceneName = "GameScene";

    public GameObject loadingScreen;
    public Slider loadingSlider;

    public Button skipButton;
    public float delayBeforeShow = 3f;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        canvasGroup = skipButton.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        canvasGroup.alpha = 0f;
        skipButton.interactable = false;

        StartCoroutine(ShowSkipButton());

        loadingScreen.SetActive(false);
    }

    private IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        float timeElapsed = 0f;
        float fadeDuration = 1f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        skipButton.interactable = true;
    }

    public void OnSkipClicked()
    {
        audioManager.PlaySFX(audioManager.buttonClick);

        loadingScreen.SetActive(true);

        StartCoroutine(LoadSceneAsync(nextSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
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

        loadingScreen.SetActive(false);
    }
}
