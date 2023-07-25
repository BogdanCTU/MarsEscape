using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auth: Bob
/// An enemy spawner script based on "Object Pooling" pattern
/// </summary>

public class EnemySpawner : MonoBehaviour
{

    #region Fields

    [SerializeField] private ObjectPooling _enemyPool;

    [Space]
    [Header("Object Pooling:")]
    [SerializeField] private float _spawnRadius = 10f;

    [Space]
    [SerializeField] private LayerMask _obstacleLayer;

    // TODO
    //private WaveSystem waveSystem = WaveSystem.instance;

    #endregion Fields

    #region Methods

    public void SpawnEnemy()
    {
        // Randomly generate a position around the player within the spawnRadius.
        Vector2 randomCircle = Random.insideUnitCircle.normalized * _spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        // Check if the spawn position is valid (not inside an obstacle).
        if (!IsObstacleBlocking(spawnPosition))
        {
            GameObject newEnemy = _enemyPool.ActivateAndGetObject();
            newEnemy.transform.position = spawnPosition;
        }
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
