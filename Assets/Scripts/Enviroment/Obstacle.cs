using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{

    #region Fields

    public float maxHealth = 250;
    public float currentHealth;
    public GameObject explosionPS;

    [Space]
    [Header("UI Elements:")]
    public GameObject canvas;
    public RectTransform healthRectTransform;

    #endregion Fields

    #region Mono

    void Start()
    {
        currentHealth = maxHealth;
    }

    #endregion Mono

    #region Methods

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        SetHealtBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void SetHealtBar()
    {
        if(!canvas.activeInHierarchy)
            canvas.SetActive(true);

        // Calculate the percentage of remaining health.
        float healthPercentage = currentHealth / maxHealth;
        
        // Get the original size of the health bar RectTransform.
        Vector2 originalSize = healthRectTransform.sizeDelta;

        // Calculate the new width based on the health percentage.
        float newWidth = 100 * healthPercentage;

        // Set the new size for the health bar RectTransform.
        healthRectTransform.sizeDelta = new Vector2(newWidth, originalSize.y);
    }

    void Die()
    {
        SoundManager.instance.ExplosionSound();
        Destroy(gameObject);
        Instantiate(explosionPS, this.transform.position, this.transform.rotation);
        CameraMovement.instance.ShakeCamera();
    }

    #endregion Methods

}
