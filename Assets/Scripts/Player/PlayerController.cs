using UnityEngine;

/// <summary>
/// Auth : Bob
/// A simple script that controls player stats
/// </summary>

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(PlayerShooting))]
public class PlayerController : MonoBehaviour
{

    #region Fields

    public static PlayerController instance;

    [Space]
    [Header("Required Components:")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerShooting _plyerShooting;

    [Space]
    [Header("Player Stats:")]
    public int damage = 0;
    public int life = 100;
    public int maxLife = 100;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion Mono

    #region Methods

    #region PowerUps

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

        UIContainer.Instance.playerDamageText.text = "Damage: " + (damage);
    }

    public void SpeedBoost(int increase)
    {
        _playerMovement.SetMovementSpeed(_playerMovement.GetMovementSpeed() + increase);
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

}
