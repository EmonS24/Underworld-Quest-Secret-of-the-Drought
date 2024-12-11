using System.Collections;
using UnityEngine;

public class AnimalSFX : MonoBehaviour
{
    public AudioSource audioSource; 
    public float loopDelay;  

    private void Start()
    {
        StartCoroutine(PlaySFX());
    }

IEnumerator PlaySFX()
{
    while (true)
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is missing!");
            yield break; 
        }

        if (audioSource.clip == null)
        {
            Debug.LogError("AudioClip is missing on AudioSource!");
            yield break; 
        }

        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + loopDelay);
    }
}

}
