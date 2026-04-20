//using UnityEngine;

//public class SpiderProjectile : MonoBehaviour
//{
//    [Header("Movement")]
//    [SerializeField] private float speed = 20f;
//    [SerializeField] private float lifeTime = 5f;

//    private Rigidbody rb;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    private void Start()
//    {
//        if (rb != null)
//        {
//            rb.linearVelocity = -transform.forward * speed;
//        }

//        Destroy(gameObject, lifeTime);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Enemy")) return; // optional, falls Spinne/Enemy nicht sofort getroffen werden soll

//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private bool invertDirection = false;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (rb != null)
        {
            Vector3 shootDirection = invertDirection ? -transform.forward : transform.forward;
            rb.linearVelocity = shootDirection * speed;
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) return;

        Destroy(gameObject);
    }
}