using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerUI : MonoBehaviour
{
    [Header("----------Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------Audio Clip ----------")]
    public AudioClip backgroundUI;
    public AudioClip buttonClick;

    private void Start()
    {
        musicSource.clip = backgroundUI;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
