using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{

    #region Fields

    public int damageAmount = 50;
    public float speed = 10f;
    public float lifetime = 2f; // Adjust this to control how long the projectile stays before being destroyed.

    #endregion Fields

    #region Mono

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the projectile collided with an enemy or another object that can take damage.
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount + PlayerController.instance.damage); // Call the TakeDamage function on the enemy script.
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.TakeDamage(damageAmount + PlayerController.instance.damage); // Call the TakeDamage function on the object script.
        }

        // Destroy the projectile when it collides with any object.
        Destroy(gameObject);
    }

    #endregion Mono

}