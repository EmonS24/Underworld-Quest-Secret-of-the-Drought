using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private string checkpointFilePath;
    private List<string> collectedItemIDs = new List<string>();
    private List<PushableObjectData> pushableObjectPositions = new List<PushableObjectData>(); 

    private void Awake()
    {
        checkpointFilePath = Path.Combine(Application.persistentDataPath, "checkpoint.dat");
    }

    public void SaveCheckpoint(string sceneName, Vector2 position, int questProgress)
    {
        foreach (var pushableObject in FindObjectsOfType<PushableObject>())
        {
            pushableObjectPositions.Add(new PushableObjectData(pushableObject.objectID, pushableObject.transform.position.x, pushableObject.transform.position.y));
        }

        CheckpointData checkpointData = new CheckpointData(
            sceneName,
            position.x,
            position.y,
            questProgress,
            collectedItemIDs.ToArray(),
            pushableObjectPositions.ToArray() 
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

                if (checkpointData.pushableObjectPositions != null)
                {
                    foreach (var objData in checkpointData.pushableObjectPositions)
                    {
                        PushableObject obj = FindPushableObjectByID(objData.objectID);
                        if (obj != null)
                        {
                            obj.transform.position = new Vector3(objData.posX, objData.posY, obj.transform.position.z);
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

    private PushableObject FindPushableObjectByID(string objectID)
    {
        PushableObject[] pushableObjects = FindObjectsOfType<PushableObject>();
        foreach (var obj in pushableObjects)
        {
            if (obj.objectID == objectID)
            {
                return obj;
            }
        }
        return null;
    }
}
