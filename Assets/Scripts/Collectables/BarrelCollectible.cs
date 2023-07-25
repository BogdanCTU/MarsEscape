using UnityEngine;

public class BarrelCollectible : MonoBehaviour
{

    #region Fields

    [SerializeField] private int _minValue = 1;
    [SerializeField] private int _maxValue = 6;

    #endregion Fields

    #region Mono

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerCurrency>(out var _playerCurrency))
        {
            _playerCurrency.AddBarrels(Random.Range(_minValue, _maxValue));

            SoundManager.instance.PowerUpSound();

            gameObject.SetActive(false);
        }
    }

    #endregion Mono

}
