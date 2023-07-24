using UnityEngine;
using UnityEngine.UI;

public class PowerUpCollectible : MonoBehaviour
{

    #region Fields

    [Header("0 => DamageBoost")]
    [Header("1 => SpeedBoost")]
    [Header("2 => LifeBoost")]
    [Header("3 => RegenBoost")]

    [Space]
    [SerializeField] private int collectibleType = 0;
    [SerializeField] private int boost = 0;

    [Space]
    [Header("Damage Stats:")]
    [SerializeField] public int minDamageBoost = 10;
    [SerializeField] public int maxDamageBoost = 50;

    [Space]
    [Header("Speed Stats:")]
    [SerializeField] public int minSpeedBoost = 5;
    [SerializeField] public int maxSpeedBoost = 10;

    [Space]
    [Header("Life Stats:")]
    [SerializeField] public int minLifeBoost = 10;
    [SerializeField] public int maxLifeBoost = 50;

    [Space]
    [Header("Regen Stats:")]
    [SerializeField] public int minLifeRegen = 10;
    [SerializeField] public int maxLifeRegen = 50;

    [Space]
    [Header("Text UI:")]
    [SerializeField] private Text collectableText;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        SetStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            Boost(playerController);

            SoundManager.instance.PowerUpSound();

            Destroy(this.gameObject);
        }
    }

    #endregion Mono

    #region Methods

    private void SetStats()
    {
        collectibleType = Random.Range(0, 4);

        switch (collectibleType)
        {
            case 0:
                {
                    boost = Random.Range(minDamageBoost, maxDamageBoost);
                    collectableText.text = "Damage boost: " + boost;
                    break;
                }
            case 1:
                {
                    boost = Random.Range(minSpeedBoost, maxSpeedBoost);
                    collectableText.text = "Speed boost: " + boost;
                    break;
                }
            case 2:
                {
                    boost = Random.Range(minLifeBoost, maxLifeBoost);
                    collectableText.text = "Life boost: " + boost;
                    break;
                }
            case 3:
                {
                    boost = Random.Range(minLifeRegen, maxLifeRegen);
                    collectableText.text = "Life regen: " + boost;
                    break;
                }
            default:
                {
                    boost = Random.Range(minDamageBoost, maxDamageBoost);
                    collectableText.text = "Damage boost: " + boost;
                    break;
                }
        }
    }

    private void Boost(PlayerController playerController)
    {
        switch (collectibleType)
        {
            case 0:
                {
                    playerController.DamageBoost(boost);
                    break;
                }
            case 1:
                {
                    playerController.SpeedBoost(boost);
                    break;
                }
            case 2:
                {
                    playerController.LifeBoost(boost);
                    break;
                }
            case 3:
                {
                    playerController.LifeRegen(boost);
                    break;
                }
            default:
                {
                    playerController.DamageBoost(boost);
                    break;
                }
        }
    }

    #endregion Methods

}
