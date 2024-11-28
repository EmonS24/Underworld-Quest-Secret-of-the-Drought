[System.Serializable]
public class VolumeData
{
    public float musicVolume;
    public float sfxVolume;

    public VolumeData(float musicVolume, float sfxVolume)
    {
        this.musicVolume = musicVolume;
        this.sfxVolume = sfxVolume;
    }
}
