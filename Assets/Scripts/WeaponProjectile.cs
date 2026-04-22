using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 20f;                    // Projectile speed
    [SerializeField] private float lifeTime = 3f;                  // Lifetime before auto-destroy
    [SerializeField] private float inheritedVelocityIntensity = 1f; // Inherited velocity multiplier

    [Header("Damage")]
    [SerializeField] private int damage = 1;                       // Damage to deal to enemies

    [Header("Despawn")]
    [SerializeField] private LayerMask despawnMask;                // Layers that destroy projectile on contact

    private bool isDespawning;                                     // Prevents multiple despawn calls
    private Vector3 inheritedVelocity;                             // Velocity inherited from shooter

    private void Start()
    {
        Destroy(gameObject, lifeTime);                             // Auto-destroy after lifetime
    }

    private void Update()
    {
        if (isDespawning) return;                                  // Skip if already despawning

        Vector3 moveDirection = transform.forward * speed;         // Base forward movement
        Vector3 finalVelocity = moveDirection + (inheritedVelocity * inheritedVelocityIntensity); // Combine velocities

        transform.position += finalVelocity * Time.deltaTime;      // Apply movement
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDespawning) return;                                  // Skip if already despawning

        if (other.CompareTag("Enemy"))                             // Hit enemy
        {
            EnemyController enemyHealth = other.GetComponent<EnemyController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);                    // Deal damage
            }

            isDespawning = true;
            Destroy(gameObject);                                   // Destroy on enemy hit
            return;
        }

        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0) // Hit despawn layer
        {
            isDespawning = true;
            Destroy(gameObject);                                   // Destroy on layer contact
        }
    }

    public void SetInheritedVelocity(Vector3 velocity)              // Set inherited velocity from shooter
    {
        inheritedVelocity = velocity;
    }
}