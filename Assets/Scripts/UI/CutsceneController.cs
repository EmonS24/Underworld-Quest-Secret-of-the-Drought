using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public string nextSceneName;  
    private VideoPlayer videoPlayer;

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
        SceneManager.LoadSceneAsync(nextSceneName);
    }
}
