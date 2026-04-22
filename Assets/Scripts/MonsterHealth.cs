using UnityEngine;

// Handles the monster's health, damage intake, and destruction on death.
public class MonsterHealth : MonoBehaviour
{
    // Maximum health value the monster starts with.
    public int maxHealth = 3;
    // Current health during gameplay.
    public int currentHealth;

    void Start()
    {
        // Initialize current health when the monster is created.
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Reduce current health by the incoming damage value.
        currentHealth -= damage;

        // Destroy the monster if its health reaches zero or below.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Remove the monster object from the scene.
        Destroy(gameObject);
    }
}