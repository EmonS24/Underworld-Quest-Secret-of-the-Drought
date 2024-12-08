using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public string nextSceneName;
    public CheckpointManager checkpointManager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointManager.ClearCheckpoint();
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
