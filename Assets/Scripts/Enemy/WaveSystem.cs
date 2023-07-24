using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    #region Fields

    public static WaveSystem instance;

    [Space]
    [Header("Wave:")]
    public int currentWaveIndex = 0;
    public int countdown = 81;

    [Space]
    [Header("Enemy Stats:")]
    public int enemyToSpawn = 8;
    public int enemySpawnCount = 0;
    public int currentEnemiesCount = 0;
    public int enemyMaxCount = 10;

    public int enemiesDefeated = 0;
    public int enemiesTotalDefeated = 0;

    private EnemySpawner _enemySpawner;
    private WaitForSeconds _second = new WaitForSeconds(1);
    private WaitForSeconds _twoSeconds = new WaitForSeconds(2);

    #endregion Fields

    #region Mono

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        StartNextWave();
    }

    #endregion Mono

    #region Methods

    public void StartNextWave()
    {
        StopAllCoroutines();

        // Start the next wave
        countdown = 81;
        currentWaveIndex++;
        enemiesDefeated = 0;
        enemySpawnCount = 0;
        enemyToSpawn += 2;

        StartCoroutine(NextWaveCountdown());
        StartCoroutine(EnemySpawnRate());

        // Refresh UI
        UIContainer.Instance.waveCountText.text = "Wave: " + currentWaveIndex;
    }

    private IEnumerator NextWaveCountdown()
    {
        yield return new WaitForSeconds(1);

        // Reduce one second
        if(countdown > 0)
            countdown--;

        // Refresh UI
        UIContainer.Instance.waveTimeCountdownText.text = "Next wave: " + countdown + "s";

        if (countdown > 0)
            StartCoroutine(NextWaveCountdown());
        else
            StartNextWave();
    }

    private IEnumerator EnemySpawnRate()
    {
        yield return _twoSeconds;

        if (currentEnemiesCount < enemyMaxCount && enemySpawnCount < enemyToSpawn && this.gameObject.activeInHierarchy == true)
        {
            _enemySpawner.SpawnEnemy(currentWaveIndex);
            enemySpawnCount++;
            currentEnemiesCount++;
        }

        StartCoroutine(EnemySpawnRate());
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        enemiesTotalDefeated++;
        currentEnemiesCount--;

        UIContainer.Instance.emeniesDefeatedText.text = "Enemies defeated: " + enemiesTotalDefeated;

        if (enemiesDefeated == enemyToSpawn)
        {
            StartNextWave();
        }
    }

    #endregion Methods

}
