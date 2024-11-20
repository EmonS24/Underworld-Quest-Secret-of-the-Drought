using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Source ----")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---- Audio Clip ----")]
        public AudioClip backgroundUI;
    public AudioClip buttonClick;
    public AudioClip footsteps;
    public AudioClip jumpStep;
    public AudioClip jumpGround;

    
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
