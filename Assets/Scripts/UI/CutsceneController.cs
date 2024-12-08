using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public string nextSceneName;
    private VideoPlayer videoPlayer;

    public GameObject loadingScreen;
    public Slider loadingSlider;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(PlayVideoWithDelay());

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private IEnumerator PlayVideoWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
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
