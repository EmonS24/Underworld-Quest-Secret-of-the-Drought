[System.Serializable]
public class CheckpointData
{
    public string sceneName;
    public float posX;
    public float posY;

    public CheckpointData(string sceneName, float posX, float posY)
    {
        this.sceneName = sceneName;
        this.posX = posX;
        this.posY = posY;
    }
}
