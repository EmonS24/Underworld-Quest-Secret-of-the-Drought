using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private string checkpointFilePath;
    private List<string> collectedItemIDs = new List<string>();

    private void Start()
    {
        checkpointFilePath = Application.persistentDataPath + "/checkpoint.json";
    }

public void SaveCheckpoint(string sceneName, Vector2 position, int questProgress)
{
    if (SceneManager.GetActiveScene().name == sceneName)
    {
        CheckpointData checkpointData = new CheckpointData(
            sceneName,
            position.x,
            position.y,
            questProgress,
            collectedItemIDs.ToArray() 
        );

        string json = JsonUtility.ToJson(checkpointData);
        File.WriteAllText(checkpointFilePath, json);
        Debug.Log("Checkpoint saved: " + json);
    }
}

public void AddCollectedItem(string itemID)
{
    if (!collectedItemIDs.Contains(itemID))
    {
        collectedItemIDs.Add(itemID);
    }
}

public CheckpointData LoadCheckpoint()
{
    if (File.Exists(checkpointFilePath))
    {
        string json = File.ReadAllText(checkpointFilePath);
        CheckpointData checkpointData = JsonUtility.FromJson<CheckpointData>(json);

        if (checkpointData.collectedItems != null)
        {
            foreach (string itemID in checkpointData.collectedItems)
            {
                CollectableItem[] items = FindObjectsOfType<CollectableItem>();
                foreach (CollectableItem item in items)
                {
                    if (item.itemID == itemID)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
            }
        }

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


