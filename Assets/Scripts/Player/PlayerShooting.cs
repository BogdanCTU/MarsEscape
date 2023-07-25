using UnityEngine;

/// <summary>
/// Auth : Bob
/// A simple script that controls player shooting feature
/// </summary>

public class PlayerShooting : MonoBehaviour
{

    #region Fields

    [Space]
    [Header("Projectiles:")]
    [SerializeField] private int _currentProjectile = 0;
    [SerializeField] private Transform _projectilesSpawnPos;
    [SerializeField] private Transform _projectilesSpawnHolder;
    [SerializeField] private float _shootCooldown = 0.2f;
    [SerializeField] private bool _canShoot = true;

    [SerializeField] private ObjectPoolingMultiple _objectPoolingMultiple;

    private Camera mainCamera;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        // Getting Main camera
        mainCamera = Camera.main;
        if (mainCamera == null)
            mainCamera = FindFirstObjectByType<Camera>();
    }

    private void Start()
    {
        // Adding Projectile damage on Player Stats
        ProjectilePlayer playerProjectile = GetCurrentPlayerProjectile();
        if (playerProjectile != null)
            PlayerController.instance.DamageBoost(playerProjectile.GetDamageAmount());
    }

    void FixedUpdate()
    {
        RotateProjectileHolder();

        // Shooting projectile on spacebar press and when cooldown is over.
        if (_canShoot == true && Input.GetKey(KeyCode.Mouse0))
        {
            ShootProjectile();

            _canShoot = false;
            Invoke(nameof(ResetShootCooldown), _shootCooldown);
        }
    }

    #endregion Mono

    #region Methods

    void ShootProjectile()
    {
        SoundManager.instance.LaserSound();

        // Spawn the projectile at the player's position and rotation.
        GameObject newProjectile = _objectPoolingMultiple.SpawnAndGetObject(_objectPoolingMultiple._itemsToPool[_currentProjectile].objectTag);
        newProjectile.transform.position = _projectilesSpawnPos.transform.position;
        newProjectile.transform.rotation = _projectilesSpawnPos.transform.rotation;

        // Set the projectile's initial velocity in the direction the spawner is looking (towards the mouse).
        ProjectilePlayer projectile = newProjectile.GetComponent<ProjectilePlayer>();
        newProjectile.GetComponent<Rigidbody>().velocity = _projectilesSpawnPos.forward * projectile.GetSpeed();
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
        _projectilesSpawnHolder.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void ResetShootCooldown()
    {
        _canShoot = true;
    }

    #region Getters/Setters

    public void SetCurrentProjectile(int projectile)
    {
        _currentProjectile = projectile;
    }

    public int GetCurrentProjectile()
    {
        return _currentProjectile;
    }

    public void SetShootCooldown(float shootCooldown)
    {
        _shootCooldown = shootCooldown;
    }

    public float GetShootCooldown()
    {
        return _shootCooldown;
    }

    public ProjectilePlayer GetCurrentPlayerProjectile()
    {
        if (_objectPoolingMultiple._itemsToPool[_currentProjectile].objectToPool.TryGetComponent(out ProjectilePlayer projectile))
            return projectile;
        return null;
    }

    #endregion Getters/Setters

    #endregion Methods

}
