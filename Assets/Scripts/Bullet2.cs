using UnityEngine;

// Handles projectile movement, spider damage, optional explosion spawning,
// and detaching a trail effect when the bullet is destroyed.
public class Bullet2 : MonoBehaviour
{
    [Header("Damage")]
    // Amount of damage dealt to the target.
    public float damage = 100f;
    // Forward movement speed of the bullet.
    public float speed = 20f;

    [Header("Explosion")]
    // Explosion effect that is spawned on a valid kill or fallback hit.
    public GameObject explosionPrefab;   // Stays for hit/explosion effects.

    [Header("Trail / Schweif")]
    // Existing trail effect prefab that gets attached to the bullet on spawn.
    public GameObject trailEffectPrefab;
    // Local offset for positioning the trail relative to the bullet.
    public Vector3 trailLocalOffset = new Vector3(0f, 0f, -0.5f);
    // Lifetime of the detached trail after the bullet is destroyed.
    public float detachedTrailLifetime = 1.5f;

    // Reference to the instantiated trail effect object.
    private GameObject spawnedTrail;

    void Start()
    {
        // Spawn and attach the trail effect if a prefab was assigned.
        if (trailEffectPrefab != null)
        {
            spawnedTrail = Instantiate(trailEffectPrefab, transform);
            spawnedTrail.transform.localPosition = trailLocalOffset;
            spawnedTrail.transform.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        // Move the bullet forward every frame.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only apply damage logic when the collided object is tagged as Enemy.
        if (other.CompareTag("Enemy"))
        {
            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
            if (enemyHealth != null)
            {
                // Store the health before damage so a kill can be detected afterward.
                float healthBefore = enemyHealth.currentHealth;
                enemyHealth.TakeDamage(damage);

                // Spawn an explosion only when the enemy actually died from this hit.
                if (healthBefore > 0 && enemyHealth.currentHealth <= 0)
                {
                    SpawnExplosion(other.transform.position);
                }
            }
            else
            {
                // Fallback behavior if no SpiderHealth component is found.
                Destroy(other.gameObject);
                SpawnExplosion(other.transform.position);
            }
        }

        // Detach the trail and destroy the bullet after any trigger collision.
        DetachTrail();
        Destroy(gameObject);
    }

    void SpawnExplosion(Vector3 position)
    {
        // Create the explosion effect if a prefab is available.
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            // Remove the temporary explosion object after a short time.
            Destroy(explosion, 2f);
        }
    }

    void DetachTrail()
    {
        // Stop if no trail was spawned.
        if (spawnedTrail == null)
            return;

        // Detach the trail so it can finish playing independently.
        spawnedTrail.transform.parent = null;

        // Stop all particle systems from emitting new particles.
        ParticleSystem[] particleSystems = spawnedTrail.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        // Destroy the detached trail object after its remaining lifetime.
        Destroy(spawnedTrail, detachedTrailLifetime);
        spawnedTrail = null;
    }
}