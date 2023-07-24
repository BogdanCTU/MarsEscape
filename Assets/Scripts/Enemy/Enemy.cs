using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    #region Fields

    [Space]
    [Header("Enemy Stats:")]
    public int maxHealth = 100;
    public int damage = 10;
    public float currentHealth;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject explosionPS;

    [Space]
    [Header("UI Elements:")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform healthRectTransform;

    [Space]
    [Header("Enemy Shoot:")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform projectileSpawnHolder;
    [SerializeField] private int shootingRadius = 5;
    [SerializeField] private int shootingCooldown = 2;
    [SerializeField] private bool canShoot = true;

    private Transform player;
    private WaveSystem waveSystem = WaveSystem.instance;

    #endregion Fields

    #region Mono

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // If the player exists and is within the agent's stopping distance, follow the player.
            navMeshAgent.SetDestination(player.position);

            // Check if the player is within the shooting radius and if enough time has passed since the last shot.
            if (Vector3.Distance(transform.position, player.position) <= shootingRadius && canShoot == true)
            {
                canShoot = false;
                Invoke(nameof(ShootCooldown), shootingCooldown);

                ShootProjectile();
            }
        }
    }

    #endregion Mono

    #region Methods

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        SetHealtBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void SetHealtBar()
    {
        if (!canvas.activeInHierarchy)
            canvas.SetActive(true);

        // Calculate the percentage of remaining health.
        float healthPercentage = (float)(currentHealth / maxHealth);

        // Get the original size of the health bar RectTransform.
        Vector2 originalSize = healthRectTransform.sizeDelta;

        // Calculate the new width based on the health percentage.
        float newWidth = 100 * healthPercentage;

        // Set the new size for the health bar RectTransform.
        healthRectTransform.sizeDelta = new Vector2(newWidth, originalSize.y);
    }

    private void ShootProjectile()
    {
        SoundManager.instance.LaserSound();

        // Calculate the direction from the enemy to the player.
        Vector3 directionToPlayer = (player.position - projectileSpawnPoint.position).normalized;

        // Calculate the rotation needed to face the player's direction.
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

        // Set the projectile spawn point's rotation to the target rotation.
        projectileSpawnHolder.rotation = targetRotation;

        // Instantiate the projectile at the projectile spawn point's position and rotation.
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Set the projectile's initial velocity towards the player.
        ProjectileEnemy projectile = newProjectile.GetComponent<ProjectileEnemy>();
        newProjectile.GetComponent<Rigidbody>().velocity = directionToPlayer * projectile.speed;
        projectile.damageAmount = damage;
    }

    private void ShootCooldown()
    {
        canShoot = true;
    }

    void Die()
    {
        SoundManager.instance.ExplosionSound();
        Destroy(gameObject);
        Instantiate(explosionPS, this.transform.position, this.transform.rotation);
        CameraMovement.instance.ShakeCamera();

        waveSystem.EnemyDefeated();
    }

    
    #endregion Methods

}
