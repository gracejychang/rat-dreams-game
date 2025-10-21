using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs; // Array of objects we can spawn
    [SerializeField] private float spawnInterval = 1.5f;
    [SerializeField] private int numberOfColumns = 5;
    [SerializeField] private float spacing = 1f; // horizontal spacing between columns

    private float timer = 0f;
    private float screenTopY = 6f;
    private float screenLeftX = -2f;
    private float screenRightX = 2f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // Choose a random prefab
        GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Length)];

        // Choose a random column
        int randomColumn = Random.Range(0, numberOfColumns);
        float xPos = screenLeftX + (randomColumn * spacing);

        // clamp xPos to screen width
        xPos = Mathf.Clamp(xPos, screenLeftX, screenRightX);
        Vector3 spawnPosition = new Vector3(xPos, screenTopY, 0);

        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObject.SetActive(true);
    }
}
