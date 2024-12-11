using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---- Audio Source ----")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---- Audio Clip ----")]
    public AudioClip background;
    public AudioClip buttonClick;
    public AudioClip footsteps;
    public AudioClip jumpStep;
    public AudioClip jumpGround;
    public AudioClip BoxSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
