[System.Serializable]
public class CheckpointData
{
    public string sceneName;
    public float posX;
    public float posY;
    public int questProgress;
    public string[] collectedItems;
    public PushableObjectData[] pushableObjectPositions; 

    public CheckpointData(string sceneName, float posX, float posY, int questProgress, string[] collectedItems, PushableObjectData[] pushableObjectPositions)
    {
        this.sceneName = sceneName;
        this.posX = posX;
        this.posY = posY;
        this.questProgress = questProgress;
        this.collectedItems = collectedItems;
        this.pushableObjectPositions = pushableObjectPositions;
    }
}

[System.Serializable]
public class PushableObjectData
{
    public string objectID;
    public float posX;
    public float posY;

    public PushableObjectData(string objectID, float posX, float posY)
    {
        this.objectID = objectID;
        this.posX = posX;
        this.posY = posY;
    }
}
