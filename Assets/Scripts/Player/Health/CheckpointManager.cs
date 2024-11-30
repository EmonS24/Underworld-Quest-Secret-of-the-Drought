using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private string checkpointFilePath;
    private List<string> collectedItemIDs = new List<string>();

    private void Awake()
    {
        checkpointFilePath = Path.Combine(Application.persistentDataPath, "checkpoint.dat");
    }

    public void SaveCheckpoint(string sceneName, Vector2 position, int questProgress)
    {
        CheckpointData checkpointData = new CheckpointData(
            sceneName,
            position.x,
            position.y,
            questProgress,
            collectedItemIDs.ToArray()
        );
        using (FileStream file = File.Create(checkpointFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, checkpointData);
        }
        Debug.Log($"Checkpoint saved successfully at {checkpointFilePath}");
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
            using (FileStream file = File.Open(checkpointFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CheckpointData checkpointData = (CheckpointData)formatter.Deserialize(file);
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
                Debug.Log($"Checkpoint loaded from {checkpointFilePath}");
                return checkpointData;
            }
        }
        else
        {
            Debug.Log($"No checkpoint file found at {checkpointFilePath}");
        }

        return null;
    }

    public void LoadCollectedItems(string[] collectedItems)
    {
        collectedItemIDs = new List<string>(collectedItems);
    }

    public void ClearCheckpoint()
    {
        if (File.Exists(checkpointFilePath))
        {
            File.Delete(checkpointFilePath);
            Debug.Log("Checkpoint file deleted.");
        }
        else
        {
            Debug.Log("No checkpoint file to clear.");
        }
    }
}
