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
    [Header("Generic Stats:")]
    [SerializeField] private int _collectibleType = 0;
    [SerializeField] private int _despawnTime = 20;
    [SerializeField] private int _boost = 0;
    [SerializeField] public int _minBoost = 10;
    [SerializeField] public int _maxBoost = 50;

    [Space]
    [Header("Text UI:")]
    [SerializeField] private Text collectableText;

    #endregion Fields

    #region Mono

    private void OnEnable()
    {
        Invoke(nameof(DespawnGameObject), _despawnTime);

        SetStats();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            Boost(playerController);

            SoundManager.instance.PowerUpSound();

            gameObject.SetActive(false);
        }
    }

    #endregion Mono

    #region Methods

    private void DespawnGameObject()
    {
        gameObject.SetActive(false);
    }

    private void SetStats()
    {
        _boost = Random.Range(_minBoost, _maxBoost);

        switch (_collectibleType)
        {
            case 0:
                {
                    collectableText.text = "Damage boost: " + _boost;
                    break;
                }
            case 1:
                {
                    collectableText.text = "Speed boost: " + _boost;
                    break;
                }
            case 2:
                {
                    collectableText.text = "Life boost: " + _boost;
                    break;
                }
            case 3:
                {
                    collectableText.text = "Life regen: " + _boost;
                    break;
                }
            default:
                {
                    collectableText.text = "Damage boost: " + _boost;
                    break;
                }
        }
    }

    private void Boost(PlayerController playerController)
    {
        switch (_collectibleType)
        {
            case 0:
                {
                    playerController.PlayerDamageBoost(_boost);
                    break;
                }
            case 1:
                {
                    playerController.PlayerSpeedBoost(_boost);
                    break;
                }
            case 2:
                {
                    playerController.PlayerLifeBoost(_boost);
                    break;
                }
            case 3:
                {
                    playerController.PlayerLifeRegeneration(_boost);
                    break;
                }
            default:
                {
                    playerController.PlayerDamageBoost(_boost);
                    break;
                }
        }
    }

    #endregion Methods

}
