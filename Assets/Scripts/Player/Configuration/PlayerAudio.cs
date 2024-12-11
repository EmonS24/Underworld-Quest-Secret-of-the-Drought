using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    AudioManager audioManager;
    public PlayerVar player;

    private bool isBoxSoundPlaying = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        HandleBoxAudio();
    }

    private void HandleBoxAudio()
    {
        if (player.isGrabbing && player.isMove)
        {
            if (!isBoxSoundPlaying) 
            {
                PlayBoxAudio();
                isBoxSoundPlaying = true;
            }
        }
        else
        {
            if (isBoxSoundPlaying) 
            {
                StopBoxAudio();
                isBoxSoundPlaying = false;
            }
        }
    }

    public void PlayWalkAudio()
    {
        audioManager.PlaySFX(audioManager.footsteps);
    }

    public void PlayBoxAudio()
    {
        if (!audioManager.SFXSource.isPlaying) 
        {
            audioManager.SFXSource.clip = audioManager.BoxSound;
            audioManager.SFXSource.Play();
        }
    }

    public void StopBoxAudio()
    {
        if (audioManager.SFXSource.clip == audioManager.BoxSound) 
        {
            audioManager.SFXSource.Stop();
        }
    }
}
