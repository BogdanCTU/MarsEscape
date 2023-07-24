using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
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
        // Check if the projectile collided with the player
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.DamagePlayer(damageAmount); // Call the TakeDamage function on the player script.
        }

        Obstacle obstacle = other.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.TakeDamage(damageAmount); // Call the TakeDamage function on the object script.
        }

        // Destroy the projectile when it collides with any object.
        Destroy(gameObject);
    }

    #endregion Mono

}
