////using UnityEngine;

////public class WeaponProjectile : MonoBehaviour
////{
////    [Header("Movement")]
////    [SerializeField] private float speed = 20f;
////    [SerializeField] private float lifeTime = 3f;

////    [Header("Damage")]
////    [SerializeField] private int damage = 1;

////    [Header("Despawn")]
////    [SerializeField] private LayerMask despawnMask;

////    private float lifeTimer;
////    private bool isDespawning;

////    private void Start()
////    {
////        lifeTimer = lifeTime;
////    }

////    private void Update()
////    {
////        if (isDespawning)
////            return;

////        transform.position += transform.forward * speed * Time.deltaTime;

////        lifeTimer -= Time.deltaTime;
////        if (lifeTimer <= 0f)
////        {
////            Destroy(gameObject);
////        }
////    }

////    private void OnTriggerEnter(Collider other)
////    {
////        if (isDespawning)
////            return;

////        if (other.CompareTag("Enemy"))
////        {
////            EnemyController enemyHealth = other.GetComponent<EnemyController>();

////            if (enemyHealth != null)
////            {
////                enemyHealth.TakeDamage(damage);
////            }
////        }

////        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
////        {
////            isDespawning = true;
////            Destroy(gameObject);
////        }
////    }

////    public int GetDamage()
////    {
////        return damage;
////    }
////}

//////using UnityEngine;

//////public class WeaponProjectile : MonoBehaviour
//////{
//////    [Header("Movement")]
//////    [SerializeField] private float speed = 20f;
//////    [SerializeField] private float lifeTime = 3f;
//////    [SerializeField] private float playerSpeedInfluence = 1f;

//////    [Header("Damage")]
//////    [SerializeField] private int damage = 1;

//////    [Header("Despawn")]
//////    [SerializeField] private LayerMask despawnMask;

//////    private float lifeTimer;
//////    private bool isDespawning;
//////    private Vector3 inheritedVelocity;

//////    private void Start()
//////    {
//////        lifeTimer = lifeTime;

//////        GameObject player = GameObject.FindGameObjectWithTag("Player");
//////        if (player != null)
//////        {
//////            Rigidbody rb = player.GetComponent<Rigidbody>();
//////            if (rb != null)
//////            {
//////                inheritedVelocity = rb.linearVelocity * playerSpeedInfluence;
//////            }
//////        }
//////    }

//////    private void Update()
//////    {
//////        if (isDespawning)
//////            return;

//////        transform.position += (transform.forward * speed + inheritedVelocity) * Time.deltaTime;

//////        lifeTimer -= Time.deltaTime;
//////        if (lifeTimer <= 0f)
//////        {
//////            Destroy(gameObject);
//////        }
//////    }

//////    private void OnTriggerEnter(Collider other)
//////    {
//////        if (isDespawning)
//////            return;

//////        if (other.CompareTag("Enemy"))
//////        {
//////            EnemyController enemyHealth = other.GetComponent<EnemyController>();

//////            if (enemyHealth != null)
//////            {
//////                enemyHealth.TakeDamage(damage);
//////            }
//////        }

//////        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
//////        {
//////            isDespawning = true;
//////            Destroy(gameObject);
//////        }
//////    }

//////    public int GetDamage()
//////    {
//////        return damage;
//////    }
//////}

//using UnityEngine;

//public class WeaponProjectile : MonoBehaviour
//{
//    [Header("Movement")]
//    [SerializeField] private float speed = 20f;
//    [SerializeField] private float lifeTime = 3f;

//    [Header("Damage")]
//    [SerializeField] private int damage = 1;

//    [Header("Despawn")]
//    [SerializeField] private LayerMask despawnMask;

//    private float lifeTimer;
//    private bool isDespawning;

//    private void Start()
//    {
//        lifeTimer = lifeTime;
//    }

//    private void Update()
//    {
//        if (isDespawning)
//            return;

//        transform.position += transform.forward * speed * Time.deltaTime;

//        lifeTimer -= Time.deltaTime;
//        if (lifeTimer <= 0f)
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (isDespawning)
//            return;

//        if (other.CompareTag("Enemy"))
//        {
//            EnemyController enemyHealth = other.GetComponent<EnemyController>();

//            if (enemyHealth != null)
//            {
//                enemyHealth.TakeDamage(damage);
//            }
//        }

//        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
//        {
//            isDespawning = true;
//            Destroy(gameObject);
//        }
//    }

//    public int GetDamage()
//    {
//        return damage;
//    }
//}

using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float inheritedVelocityIntensity = 1f;

    [Header("Damage")]
    [SerializeField] private int damage = 1;

    [Header("Despawn")]
    [SerializeField] private LayerMask despawnMask;

    private bool isDespawning;
    private Vector3 inheritedVelocity;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (isDespawning) return;

        Vector3 moveDirection = transform.forward * speed;
        Vector3 finalVelocity = moveDirection + (inheritedVelocity * inheritedVelocityIntensity);

        transform.position += finalVelocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDespawning) return;

        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyHealth = other.GetComponent<EnemyController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            isDespawning = true;
            Destroy(gameObject);
            return;
        }

        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
        {
            isDespawning = true;
            Destroy(gameObject);
        }
    }

    public void SetInheritedVelocity(Vector3 velocity)
    {
        inheritedVelocity = velocity;
    }
}