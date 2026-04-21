//using UnityEngine;

//public class creatureshot_damage : MonoBehaviour
//{
//    [Header("Damage")]
//    [SerializeField] private int damage = 10;

//    [Header("Lifetime")]
//    [SerializeField] private float lifeTime = 5f;

//    private void Start()
//    {
//        Destroy(gameObject, lifeTime);
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

//        if (playerHealth == null)
//        {
//            playerHealth = collision.gameObject.GetComponentInParent<PlayerHealth>();
//        }

//        if (playerHealth != null)
//        {
//            playerHealth.TakeDamage(damage);
//        }

//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class creatureshot_damage : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] private float lifetime = 5f;

    private bool hasHit = false;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        Debug.Log("Projectile Trigger mit: " + other.name + " | Tag: " + other.tag);

        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            hasHit = true;
            Debug.Log("PlayerHealth gefunden auf Parent. Damage wird angewendet: " + damage);
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}