using System;
using UnityEngine;

// Handles projectile movement, enemy damage, optional trail effects,
// and spawning an explosion effect when an enemy is destroyed.
public class BulletDamage : MonoBehaviour
{
    [Header("Stats")]
    // Amount of damage this bullet deals on hit.
    public float damage = 100f;
    // Forward movement speed of the bullet.
    public float speed = 20f;

    [Header("Effects")]
    // Optional explosion effect that is spawned when an enemy dies.
    public GameObject explosionPrefab;
    // Determines whether the TrailRenderer should be enabled.
    public bool useTrail = false;

    // Cached TrailRenderer reference on this bullet.
    private TrailRenderer trail;

    private void Awake()
    {
        // Try to get the trail renderer attached to this projectile.
        trail = GetComponent<TrailRenderer>();

        // If a trail exists, enable or disable it based on the inspector setting.
        if (trail != null)
        {
            trail.enabled = useTrail;
            // Clear old trail data to avoid visual artifacts on spawn.
            trail.Clear();
        }
    }

    private void Update()
    {
        // Move the bullet forward every frame.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react to colliders tagged as Enemy.
        if (other.CompareTag("Enemy"))
        {
            bool enemyDied = false;

            // Try to find an EnemyController to apply proper health-based damage.
            EnemyController enemyHealth = other.GetComponent<EnemyController>();
            if (enemyHealth != null)
            {
                enemyDied = enemyHealth.TakeDamage(Mathf.RoundToInt(damage));
            }
            else
            {
                // If no health component exists, destroy the object directly.
                Destroy(other.gameObject);
                enemyDied = true;
            }

            // Only spawn the explosion effect if the enemy was actually destroyed.
            if (enemyDied)
            {
                SpawnExplosion(other.transform.position);
            }

            // Destroy the bullet after a successful collision with an enemy.
            Destroy(gameObject);
        }
    }

    private void SpawnExplosion(Vector3 position)
    {
        // Spawn the explosion effect if one was assigned.
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            // Clean up the spawned effect after a short delay.
            Destroy(explosion, 2f);
        }
    }

    internal int GetDamage()
    {
        // Returns the bullet damage as an integer value.
        return Mathf.RoundToInt(damage);
    }
}