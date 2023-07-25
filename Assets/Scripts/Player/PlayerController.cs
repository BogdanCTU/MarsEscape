using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    #region Fields

    public static PlayerController instance;

    public PlayerMovement playerMovement;

    [Space]
    [Header("PlayerStats:")]
    public Rigidbody playerRigidbody;
    public float moveSpeed = 2f;
    public float rotationSpeed = 150f;
    public int damage = 0;
    public int life = 0;
    public int maxLife = 100;

    [Space]
    [Header("Projectiles:")]
    public int currentProjectile;
    public List<GameObject> projectiles;
    public Transform projectilesSpawnPos;
    public Transform projectilesSpawnHolder;
    public Transform projectilesContainerTransform;
    public float shootCooldown = 0.5f; // Adjust this value to control the shooting cooldown.
    public bool _canShoot = true;

    private Camera mainCamera;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        if (instance == null)
            instance = this;

        life = maxLife;

        mainCamera = Camera.main;

        if (mainCamera == null)
            mainCamera = FindFirstObjectByType<Camera>();
    }

    void FixedUpdate()
    {
        RotateProjectileHolder();

        // Shooting projectile on spacebar press and when cooldown is over.
        if (Input.GetKeyDown(KeyCode.Mouse0) && _canShoot == true)
        {
            ShootProjectile();

            _canShoot = false;
            Invoke(nameof(ResetShootCooldown), shootCooldown);
        }
    }

    #endregion Mono

    #region Methods

    void ShootProjectile()
    {
        SoundManager.instance.LaserSound();

        // Instantiate the projectile at the player's position and rotation.
        GameObject newProjectile = Instantiate(projectiles[currentProjectile], projectilesSpawnPos.position, projectilesSpawnPos.rotation, projectilesContainerTransform);

        // Set the projectile's initial velocity in the direction the spawner is looking (towards the mouse).
        ProjectilePlayer projectile = newProjectile.GetComponent<ProjectilePlayer>();
        newProjectile.GetComponent<Rigidbody>().velocity = projectilesSpawnPos.forward * projectile.speed;
    }

    void RotateProjectileHolder()
    {
        // Get the mouse position in screen space.
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to world space.
        mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.transform.position.y));

        // Calculate the direction from the player to the mouse position.
        Vector3 directionToMouse = mousePosition - transform.position;
        directionToMouse.y = 0f; // Keep the rotation in the XZ plane.

        // Calculate the rotation angle around the y-axis.
        float angle = Mathf.Atan2(directionToMouse.x, directionToMouse.z) * Mathf.Rad2Deg;

        // Rotate the projectile spawner around the y-axis to look at the mouse position.
        projectilesSpawnHolder.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    #region Setters

    private void ResetShootCooldown()
    {
        _canShoot = true;
    }

    public void DamagePlayer(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Destroy(this.gameObject);
            ShowEndScreen();
            SoundManager.instance.ExplosionSound();
        }

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + life + "/" + maxLife;
    }

    public void DamageBoost(int increase)
    {
        damage += increase;

        int projectileDamage = projectiles[currentProjectile].GetComponent<ProjectilePlayer>().damageAmount;

        // Refresh UI
        UIContainer.Instance.playerDamageText.text = "Damage: " + (damage + projectileDamage);
    }

    public void SpeedBoost(int increase)
    {
        playerMovement.SetMovementSpeed(playerMovement.GetMovementSpeed() + increase);
    }

    public void LifeBoost(int increase)
    {
        maxLife += increase;
        life += increase;

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + life + "/" + maxLife;
    }

    public void LifeRegen(int increase)
    {
        life += increase;
        life = (life > maxLife) ? maxLife : life;

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + life + "/" + maxLife;
    }

    #endregion Setters

    private void ShowEndScreen()
    {
        UIContainer.Instance.emeniesDefeatedEndText.text = "Defeated enemies: " + WaveSystem.instance.enemiesTotalDefeated;
        UIContainer.Instance.playerOilBarrelsEndText.text = "Barrels collected: " + PlayerCurrency.instance.oilBarrels;

        UIContainer.Instance.topBar.SetActive(false);
        UIContainer.Instance.bottomBar.SetActive(false);
        UIContainer.Instance.endScreen.SetActive(true);
    }

    #endregion Methods

}
