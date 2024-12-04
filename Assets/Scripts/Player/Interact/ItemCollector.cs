using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private GameObject currentItem;
    private PlayerVar player;

    public int totalItems = 5;
    private int itemsCollected = 0;
    public string nextSceneName;

    public QuestLogManager questLogManager;
    public CheckpointManager checkpointManager;


    void Start()
    {
        player = GetComponent<PlayerVar>();
    }

    void Update()
    {
        HandleCollect();
    }

    private void HandleCollect()
    {
        if (Input.GetKeyDown(KeyCode.E) && player.isGrounded)
        {
            if (currentItem != null)
            {
                itemsCollected++;

                string itemID = currentItem.GetComponent<CollectableItem>().itemID;
                checkpointManager.AddCollectedItem(itemID);

                currentItem.SetActive(false);

                questLogManager.SetQuestProgress(itemsCollected);

                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                float health = playerHealth != null ? playerHealth.health : 0f;

                checkpointManager.SaveCheckpoint(SceneManager.GetActiveScene().name, transform.position, itemsCollected, health);

                if (itemsCollected >= totalItems)
                {
                    LoadNextScene();
                }

                currentItem = null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            currentItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            currentItem = null;
        }
    }

    private void LoadNextScene()
    {
        itemsCollected = 0;
        checkpointManager.ResetCollectedItems(); 
        questLogManager.SetQuestProgress(0); 
        checkpointManager.ClearCheckpoint();
        SceneManager.LoadScene(nextSceneName);
    }
    
    public void SetItemsCollected(int progress)
    {
        itemsCollected = progress;

        if (questLogManager != null)
        {
            questLogManager.SetQuestProgress(itemsCollected);
        }
    }
}