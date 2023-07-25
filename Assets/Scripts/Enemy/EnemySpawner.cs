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
    [SerializeField] private float _minSpawnRadius = 10f; // Minimum spawn radius
    [SerializeField] private float _maxSpawnRadius = 15f; // Maximum spawn radius

    [Space]
    [SerializeField] private LayerMask _obstacleLayer;

    // TODO
    //private WaveSystem waveSystem = WaveSystem.instance;

    #endregion Fields

    #region Methods

    public void SpawnEnemy()
    {
        // Generate a random angle in radians
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);

        // Calculate the random position around the player within the spawnRadius
        Vector2 randomCircle = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * Random.Range(_minSpawnRadius, _maxSpawnRadius);

        // Convert the 2D position to 3D by adding the player's height
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0.1f, randomCircle.y) + transform.position;

        // Check if the spawn position is valid (not inside an obstacle).
        if (!IsObstacleBlocking(spawnPosition))
            _enemyPool.ActivateObjectOnPosition(spawnPosition);
        
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
