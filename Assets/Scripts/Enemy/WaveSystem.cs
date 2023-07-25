using System.Collections;
using UnityEngine;

/// <summary>
/// Auth: Bob
/// An infinite enemy spawn wave system.
/// Every wave completed the enemies become stronger based on the wave index (see Enemy script)
/// </summary>

public class WaveSystem : MonoBehaviour
{

    #region Fields

    public static WaveSystem instance;

    [Space]
    [Header("Enemy Wave:")]
    public int currentWaveIndex = 0;
    public int countdown = 60;
    private int _countdownBase = 60;

    [Space]
    [Header("Enemy Stats:")]
    public int enemyToSpawn = 8;
    public int enemySpawnCount = 0;
    public int currentEnemiesCount = 0;
    public int enemyMaxCount = 10;

    public int enemiesDefeated = 0;
    public int enemiesTotalDefeated = 0;

    [SerializeField] private WaitForSeconds _spawnRate = new WaitForSeconds(2);

    private EnemySpawner _enemySpawner;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        if(instance == null)
            instance = this;

        _countdownBase = countdown;
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
        countdown = _countdownBase;
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
        yield return _spawnRate;

        if (currentEnemiesCount < enemyMaxCount && enemySpawnCount < enemyToSpawn && gameObject.activeInHierarchy == true)
        {
            _enemySpawner.SpawnEnemy();
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
