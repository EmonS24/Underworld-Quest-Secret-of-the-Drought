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

    [SerializeField] private GameObject interactPanel; 

    void Start()
    {
        player = GetComponent<PlayerVar>();
        interactPanel.SetActive(false); 
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
                interactPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            currentItem = collision.gameObject;
            interactPanel.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            currentItem = null;
            interactPanel.SetActive(false); 
        }
    }

    private void LoadNextScene()
    {
        itemsCollected = 0;
        checkpointManager.ResetCollectedItems(); 
        questLogManager.SetQuestProgress(0); 

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