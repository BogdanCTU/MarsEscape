using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    #region Fields

    [Space]
    [Header("Enemy Spawn Stats:")]
    public List<GameObject> enemyPrefabs;
    public float spawnRadius = 10f;
    public float spawnRate = 2f;
    public int enemyHealthBoost = 50;
    public int enemyDamageBoost = 20;


    [Space]
    public LayerMask obstacleLayer;

    [Space]
    [Header("Enemy Spawn Stats:")]
    public Transform enemyContainerTransform;

    #endregion Fields

    #region Methods

    public void SpawnEnemy(int currentWaveIndex)
    {
        // Randomly generate a position around the player within the spawnRadius.
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, 0f, randomCircle.y) + transform.position;

        // Check if the spawn position is valid (not inside an obstacle).
        if (!IsObstacleBlocking(spawnPosition))
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity, enemyContainerTransform);
            Enemy enemy = newEnemy.GetComponent<Enemy>();

            currentWaveIndex--;

            // Setting enemy stats
            enemy.maxHealth = enemy.maxHealth + (enemyHealthBoost * currentWaveIndex);
            enemy.currentHealth = enemy.maxHealth;
            enemy.damage = enemy.damage + (enemyDamageBoost * currentWaveIndex);
        }
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
