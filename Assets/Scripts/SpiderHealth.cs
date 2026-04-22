using UnityEngine;

// Handles the spider's health, damage intake, death,
// and bullet damage detection through trigger collisions.
public class SpiderHealth : MonoBehaviour
{
    // Maximum health value the spider starts with.
    public float maxHealth = 100f;
    // Current remaining health during gameplay.
    public float currentHealth;

    void Start()
    {
        // Initialize current health when the spider is created.
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Reduce the spider's health by the incoming damage amount.
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Optional: effects, score update, etc.
        Destroy(gameObject);
    }

    // Used for trigger collisions, recommended for bullet objects.
    private void OnTriggerEnter(Collider other)
    {
        // React only to objects tagged as Bullet.
        if (other.CompareTag("Bullet"))
        {
            BulletDamage bullet = other.GetComponent<BulletDamage>();
            if (bullet != null)
            {
                // Apply damage defined in the bullet script.
                TakeDamage(bullet.damage);
            }
            // Destroy the bullet after the hit.
            Destroy(other.gameObject);
        }
    }
}