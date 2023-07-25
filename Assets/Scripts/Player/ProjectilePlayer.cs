using OpenCover.Framework.Model;
using System.Collections;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{

    #region Fields

    [SerializeField] private int _damageAmount = 50;
    [SerializeField] private int _speed = 2;
    [SerializeField] private WaitForSeconds _lifetime = new WaitForSeconds(10);

    #endregion Fields

    #region Mono

    void Start()
    {
        StartCoroutine(DeactivateGameObject());
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collided with an enemy or another object that can take damage.
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(PlayerController.instance.damage); // Call the TakeDamage function on the enemy script.
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.TakeDamage(PlayerController.instance.damage); // Call the TakeDamage function on the object script.
        }

        StopAllCoroutines();

        // Destroy the projectile when it collides with any object.
        gameObject.SetActive(false);
    }

    #endregion Mono

    #region Methods

    private IEnumerator DeactivateGameObject()
    {
        yield return _lifetime;

        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }

    #region Getters/Setters

    public void SetDamageAmount(int damage)
    {
        _damageAmount = damage;
    }

    public int GetDamageAmount()
    {
        return _damageAmount;
    }

    public void SetSpeed(int speed)
    {
        _speed = speed;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    #endregion Getters/Setters

    #endregion Methods

}