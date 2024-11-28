[System.Serializable]
public class CheckpointData
{
    public string sceneName;
    public float posX;
    public float posY;
    public int questProgress;
    public string[] collectedItems; 

    public CheckpointData(string sceneName, float posX, float posY, int questProgress, string[] collectedItems)
    {
        this.sceneName = sceneName;
        this.posX = posX;
        this.posY = posY;
        this.questProgress = questProgress;
        this.collectedItems = collectedItems;
    }
}
