using UnityEngine;

/// <summary>
/// Auth : Bob
/// A simple script that controls player stats
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(PlayerShooting))]
public class PlayerController : MonoBehaviour
{

    #region Fields

    public static PlayerController instance;

    [Header("Player Scriptable:")]
    [SerializeField] private EntityStats _playerStats;

    [Space]
    [Header("Required Components:")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _plyerShooting;
    [SerializeField] private Rigidbody _rigidbody;

    [Space]
    [Header("Player Stats:")]
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _maxhealth;
    [SerializeField] private int _speed;
    [SerializeField] private int _rotationSpeed;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        if (instance == null)
            instance = this;

        SetPlayerStats();
    }

    #endregion Mono

    #region Methods

    private void SetPlayerStats()
    {
        // Player Stats
        _health = _playerStats.GetHealth;
        _maxhealth = _playerStats.GetHealth;
        _damage = _playerStats.GetDamage;
        _speed = _playerStats.GetSpeed;

        // Player Movement
        _playerMovement.SetSpeed = _speed;
        _playerMovement.SetRigidbody = _rigidbody;
        _playerMovement.SetRotationSpeed = _rotationSpeed;
    }

    #region PowerUps

    public void DamagePlayer(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(this.gameObject);
            ShowEndScreen();
            SoundManager.instance.ExplosionSound();
        }

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + _health + "/" + _maxhealth;
    }

    public void PlayerDamageBoost(int increase)
    {
        _damage += increase;

        UIContainer.Instance.playerDamageText.text = "Damage: " + (_damage);
    }

    public void PlayerSpeedBoost(int increase)
    {
        _playerMovement.SetSpeed = (_playerMovement.GetSpeed + increase);
    }

    public void PlayerLifeBoost(int increase)
    {
        _maxhealth += increase;
        _health += increase;

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + _health + "/" + _maxhealth;
    }

    public void PlayerLifeRegeneration(int increase)
    {
        _health += increase;
        _health = (_health > _maxhealth) ? _maxhealth : _health;

        // Refresh UI
        UIContainer.Instance.playerHealthText.text = "Health: " + _health + "/" + _maxhealth;
    }

    #endregion PowerUps

    private void ShowEndScreen()
    {
        UIContainer.Instance.emeniesDefeatedEndText.text = "Defeated enemies: " + WaveSystem.instance.enemiesTotalDefeated;
        UIContainer.Instance.playerOilBarrelsEndText.text = "Barrels collected: " + PlayerCurrency.instance.oilBarrels;

        UIContainer.Instance.topBar.SetActive(false);
        UIContainer.Instance.bottomBar.SetActive(false);
        UIContainer.Instance.endScreen.SetActive(true);
    }

    #endregion Methods

    #region Get/Set

    public int GetDamage { get => _damage; }
    public int GetHealth { get => _health; }
    public int GetMaxhealth { get => _maxhealth; }
    public int GetSpeed { get => _speed; }

    public int SetDamage { set => _damage = value; }
    public int SetHealth { set => _health = value; }
    public int SetMaxhealth { set => _maxhealth = value; }
    public int SetSpeed { set => _speed = value; }

    #endregion Get/Set

}
