using UnityEngine;

public class PigeonSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] pigeons;

    [SerializeField]
    float spawnInterval = 5f;

    [SerializeField]
    GameObject endPoint;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        InvokeRepeating("AttemptSpawn", spawnInterval, spawnInterval);
    }

    void SpawnPigeon(Vector3 spawnPos)
    {
        int randomIndex = Random.Range(0, pigeons.Length);
        GameObject pigeon = Instantiate(pigeons[randomIndex]);

        float startY = Random.Range(startPos.y - 1f, startPos.y + 1f);
        pigeon.transform.position = new Vector3(spawnPos.x, startY, spawnPos.z);

        float scale = Random.Range(0.8f, 1.2f);
        pigeon.transform.localScale = new Vector2(scale, scale);

        float speed = Random.Range(5f, 10f);
        pigeon.GetComponent<PigeonMovement>().Fly(speed, endPoint.transform.position.x);
    }

    void AttemptSpawn()
    {
        Vector3 spawnPos = startPos + Vector3.right;
        SpawnPigeon(spawnPos);
    }
}
