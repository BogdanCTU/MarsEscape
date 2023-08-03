using OpenCover.Framework.Model;
using System.Collections;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{

    #region Fields

    [Header("Stats:")]
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
        // Check if the projectile collided with an enemy or another object that can take damage
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            // Call the TakeDamage function on the enemy script
            enemy.TakeDamage(PlayerController.instance.GetDamage);   
            StopAllCoroutines();
            gameObject.SetActive(false);
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            // Call the TakeDamage function on the object script
            obstacle.TakeDamage(PlayerController.instance.GetDamage);   
            StopAllCoroutines();
            gameObject.SetActive(false);
        }
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

    public int GetDamageAmount { get => _damageAmount; }
    public int SetDamageAmount { set => _damageAmount = value; }

    public int GetSpeed { get => _speed; }
    public int SetSpeed { set => _speed = value; }

    #endregion Getters/Setters

    #endregion Methods

}