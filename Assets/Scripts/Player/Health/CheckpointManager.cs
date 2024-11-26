using System.IO;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private string checkpointFilePath;

    private void Start()
    {
        checkpointFilePath = Application.persistentDataPath + "/checkpoint.json";
    }

    public void SaveCheckpoint(string sceneName, Vector2 position)
    {
        CheckpointData checkpointData = new CheckpointData(sceneName, position.x, position.y);
        string json = JsonUtility.ToJson(checkpointData);
        File.WriteAllText(checkpointFilePath, json);
        Debug.Log("Checkpoint saved: " + json);
    }

    public CheckpointData LoadCheckpoint()
    {
        if (File.Exists(checkpointFilePath))
        {
            string json = File.ReadAllText(checkpointFilePath);
            CheckpointData checkpointData = JsonUtility.FromJson<CheckpointData>(json);
            Debug.Log("Checkpoint loaded: " + json);
            return checkpointData;
        }
        else
        {
            Debug.Log("No checkpoint found.");
            return null;
        }
    }

    public void ClearCheckpoint()
    {
        if (File.Exists(checkpointFilePath))
        {
            File.Delete(checkpointFilePath);
            Debug.Log("Checkpoint cleared.");
        }
    }
}
