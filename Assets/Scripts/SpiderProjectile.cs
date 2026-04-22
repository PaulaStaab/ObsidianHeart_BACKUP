using UnityEngine;

// Handles spider projectile movement, automatic lifetime cleanup,
// and destruction on trigger collisions.
public class SpiderProjectile : MonoBehaviour
{
    [Header("Movement")]
    // Forward movement speed of the projectile.
    [SerializeField] private float speed = 20f;
    // Time before the projectile destroys itself automatically.
    [SerializeField] private float lifeTime = 5f;

    // Cached Rigidbody used to move the projectile.
    private Rigidbody rb;

    private void Awake()
    {
        // Cache the Rigidbody component on this projectile.
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Apply the initial movement velocity if a Rigidbody is available.
        if (rb != null)
        {
            rb.linearVelocity = -transform.forward * speed;
        }

        // Destroy the projectile automatically after its lifetime expires.
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore enemies so the projectile does not instantly collide with them.
        if (other.CompareTag("Enemy")) return;

        // Destroy the projectile on any other trigger collision.
        Destroy(gameObject);
    }
}