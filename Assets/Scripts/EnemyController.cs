using UnityEngine;

// Controls enemy health, receives damage, and destroys the enemy when health reaches zero.
public class EnemyController : MonoBehaviour
{
    [Header("Health")]
    // Maximum health value the enemy starts with.
    [SerializeField] private int maxHealth = 100;

    [Header("Damage from Tags")]
    // Reserved tag name for player projectiles.
    [SerializeField] private string playerProjectileTag = "Shoot";
    // Reserved tag name for tower bullets.
    [SerializeField] private string towerBulletTag = "Bullet";

    // Current health value during gameplay.
    private int currentHealth;

    private void Awake()
    {
        // Initialize the current health when the enemy is created.
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        // Reduce health by the incoming damage amount.
        currentHealth -= damage;

        // If health reaches zero or below, destroy the enemy.
        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        // Return false if the enemy survived the hit.
        return false;
    }

    private void Die()
    {
        // Remove the enemy object from the scene.
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        // Returns the current remaining health.
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        // Returns the configured maximum health.
        return maxHealth;
    }
}