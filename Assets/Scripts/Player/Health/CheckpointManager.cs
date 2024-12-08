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

    public void SaveCheckpoint(string sceneName, Vector2 position, int questProgress, float playerHealth)
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
            pushableObjectPositions.ToArray(),
            playerHealth
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

                // Memuat posisi pemain
                Vector2 playerPosition = new Vector2(checkpointData.posX, checkpointData.posY);
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = playerPosition;
                }

                // Memuat progress quest
                ItemCollector itemCollector = FindObjectOfType<ItemCollector>();
                if (itemCollector != null)
                {
                    itemCollector.SetItemsCollected(checkpointData.questProgress);
                }

                // Memuat collected items
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

                // Memuat posisi objek yang bisa dipindahkan
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

                // Memuat health pemain
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

    public int GetSavedQuestProgress()
    {
        if (File.Exists(checkpointFilePath))
        {
            using (FileStream file = File.Open(checkpointFilePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                CheckpointData checkpointData = (CheckpointData)formatter.Deserialize(file);
                return checkpointData.questProgress; // Ambil progress quest dari checkpoint
            }
        }
        return 0; // Jika tidak ada checkpoint, kembalikan 0
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