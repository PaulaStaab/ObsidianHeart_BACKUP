using UnityEngine;

// Controls projectile movement, lifetime, and destruction on collision.
public class ProjectileController : MonoBehaviour
{
    [Header("Movement")]
    // Movement speed of the projectile.
    [SerializeField] private float speed = 20f;
    // Time before the projectile destroys itself automatically.
    [SerializeField] private float lifeTime = 5f;
    // If true, the projectile moves in the opposite forward direction.
    [SerializeField] private bool invertDirection = false;

    // Cached Rigidbody used for projectile movement.
    private Rigidbody rb;

    private void Awake()
    {
        // Cache the Rigidbody component.
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (rb != null)
        {
            // Determine the shoot direction depending on the invert setting.
            Vector3 shootDirection = invertDirection ? -transform.forward : transform.forward;
            rb.linearVelocity = shootDirection * speed;
        }

        // Destroy the projectile automatically after its lifetime expires.
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions with enemies.
        if (other.CompareTag("Enemy")) return;

        // Destroy the projectile on any other trigger hit.
        Destroy(gameObject);
    }
}