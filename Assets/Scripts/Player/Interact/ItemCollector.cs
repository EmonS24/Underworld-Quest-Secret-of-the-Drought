using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private GameObject currentItem; 
    private PlayerVar player;

    public int totalItems = 4;
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
            FindObjectOfType<CheckpointManager>().AddCollectedItem(itemID);

            currentItem.SetActive(false);

            questLogManager.UpdateQuest($"Collect Items: {itemsCollected}/{totalItems}");
            checkpointManager.SaveCheckpoint(SceneManager.GetActiveScene().name, transform.position, itemsCollected);

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
        SceneManager.LoadScene(nextSceneName);
    }
}
