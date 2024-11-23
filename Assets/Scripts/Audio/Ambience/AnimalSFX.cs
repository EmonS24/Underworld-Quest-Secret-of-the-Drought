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
            if (audioSource && audioSource.clip)
            {
                audioSource.Play();
            }
            yield return new WaitForSeconds(audioSource.clip.length + loopDelay);
        }
    }
}
