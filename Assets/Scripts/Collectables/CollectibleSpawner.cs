using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{

    #region Fields

    [SerializeField] private ObjectPoolingMultiple _objectPoolingMultiple = null;

    [Space]
    [Header("Collectible Spawn Stats:")]
    [SerializeField] private float _spawnRadius = 10f;
    [SerializeField] private float _minCollectibleSpawnRate = 1f;
    [SerializeField] private float _maxCollectibleSpawnRate = 4f;

    [Space]
    [SerializeField] private LayerMask _obstacleLayer;

    #endregion Fields

    #region Mono

    private void Start()
    {
        Invoke(nameof(SpawnCollectible), Random.Range(_minCollectibleSpawnRate, _maxCollectibleSpawnRate));
    }

    #endregion Mono

    #region Methods

    void SpawnCollectible()
    {
        // Randomly generate a position around the player within the spawnRadius.
        Vector2 randomCircle = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        // Check if the spawn position is valid (not inside an obstacle).
        if (!IsObstacleBlocking(spawnPosition))
        {
            // Randomly select a collectible prefab from the list.
            int randomIndex = Random.Range(0, _objectPoolingMultiple._itemsToPool.Count);
            GameObject collectable = _objectPoolingMultiple.SpawnAndGetObject(_objectPoolingMultiple._itemsToPool[randomIndex].objectTag);
            collectable.transform.position = spawnPosition;
        }

        if (gameObject.activeInHierarchy == true)
            Invoke(nameof(SpawnCollectible), Random.Range(_minCollectibleSpawnRate, _maxCollectibleSpawnRate));
    }

    private bool IsObstacleBlocking(Vector3 position)
    {
        // Raycast downward from the position to check for obstacles.
        Ray ray = new Ray(position + Vector3.up * 2f, Vector3.down);

        // If an obstacle is found, return true.
        if (Physics.Raycast(ray, 1f, _obstacleLayer))
            return true;
        
        return false;
    }

    #endregion Methods

}
