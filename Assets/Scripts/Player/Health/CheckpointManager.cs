using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CheckpointManager : MonoBehaviour
{
    private string checkpointFilePath;
    private List<string> collectedItemIDs = new List<string>();
    private List<PushableObjectData> pushableObjectPositions = new List<PushableObjectData>();
    private RoomCameraManager roomCameraManager;

    private void Awake()
    {
        checkpointFilePath = Path.Combine(Application.persistentDataPath, "checkpoint.dat");
        roomCameraManager = FindObjectOfType<RoomCameraManager>();
    }

    public void SaveCheckpoint(string sceneName, Vector2 position, int questProgress, float playerHealth)
    {
        foreach (var pushableObject in FindObjectsOfType<PushableObject>())
        {
            pushableObjectPositions.Add(new PushableObjectData(pushableObject.objectID, pushableObject.transform.position.x, pushableObject.transform.position.y));
        }

        string currentRoomName = roomCameraManager != null ? roomCameraManager.GetCurrentRoomName() : null;

        CheckpointData checkpointData = new CheckpointData(
            sceneName,
            position.x,
            position.y,
            questProgress,
            collectedItemIDs.ToArray(),
            pushableObjectPositions.ToArray(),
            playerHealth,
            currentRoomName
        );

        using (FileStream file = File.Create(checkpointFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, checkpointData);
        }
        Debug.Log($"Checkpoint saved successfully at {checkpointFilePath}");
    }

    public CheckpointData LoadCheckpoint()
    {
        if (File.Exists(checkpointFilePath))
        {
            using (FileStream file = File.Open(checkpointFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CheckpointData checkpointData = (CheckpointData)formatter.Deserialize(file);

                Vector2 playerPosition = new Vector2(checkpointData.posX, checkpointData.posY);
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = playerPosition;
                }

                if (roomCameraManager != null && checkpointData.currentRoomName != null)
                {
                    roomCameraManager.SwitchToRoom(checkpointData.currentRoomName);
                }

                ItemCollector itemCollector = FindObjectOfType<ItemCollector>();
                if (itemCollector != null)
                {
                    itemCollector.SetItemsCollected(checkpointData.questProgress);
                }

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

                PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.health = checkpointData.playerHealth;
                    playerHealth.AddHealth(0);
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

    public void AddCollectedItem(string itemID)
    {
        if (!collectedItemIDs.Contains(itemID))
        {
            collectedItemIDs.Add(itemID);
            Debug.Log($"Item {itemID} added to collected items.");
        }
    }

    public int GetSavedQuestProgress()
    {
        if (File.Exists(checkpointFilePath))
        {
            using (FileStream file = File.Open(checkpointFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CheckpointData checkpointData = (CheckpointData)formatter.Deserialize(file);
                return checkpointData.questProgress; 
            }
        }
        return 0; 
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

    public void ResetCollectedItems()
    {
        collectedItemIDs.Clear(); 
        Debug.Log("Collected items reset.");
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

