using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{

    #region Fields

    [Space]
    [Header("Collectible Spawn Stats:")]
    public List<GameObject> collectiblePrefabs;
    public float spawnRadius = 10f;
    public float minCollectibleSpawnRate = 1f;
    public float maxCollectibleSpawnRate = 4f;

    [Space]
    public LayerMask obstacleLayer;

    [Space]
    [Header("Collectible Spawn Stats:")]
    public int collectibleCount = 100;
    public int collectibleSpawnCount = 0;
    public Transform collectibleContainerTransform;

    #endregion Fields

    #region Mono

    private void Start()
    {
        Invoke(nameof(SpawnCollectible), Random.Range(minCollectibleSpawnRate, maxCollectibleSpawnRate));
    }

    #endregion Mono

    #region Methods

    void SpawnCollectible()
    {
        // Randomly generate a position around the player within the spawnRadius.
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        // Check if the spawn position is valid (not inside an obstacle).
        if (!IsObstacleBlocking(spawnPosition))
        {
            collectibleSpawnCount++;

            // Randomly select a collectible prefab from the list.
            int randomIndex = Random.Range(0, collectiblePrefabs.Count);
            GameObject newCollectible = Instantiate(collectiblePrefabs[randomIndex], spawnPosition, Quaternion.identity, collectibleContainerTransform);
        }

        if (collectibleSpawnCount < collectibleCount && this.gameObject.activeInHierarchy == true)
            Invoke(nameof(SpawnCollectible), Random.Range(minCollectibleSpawnRate, maxCollectibleSpawnRate));
    }

    bool IsObstacleBlocking(Vector3 position)
    {
        // Raycast downward from the position to check for obstacles.
        Ray ray = new Ray(position + Vector3.up * 2f, Vector3.down);
        RaycastHit hit;
        float raycastDistance = 1f; // Adjust this value based on your level design.

        if (Physics.Raycast(ray, out hit, raycastDistance, obstacleLayer))
        {
            // If an obstacle is found, return true.
            return true;
        }
        return false;
    }

    #endregion Methods

}
