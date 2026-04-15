//////using UnityEngine;

//////public class WeaponProjectile : MonoBehaviour
//////{
//////    [Header("Movement")]
//////    [SerializeField] private float speed = 20f;
//////    [SerializeField] private float lifeTime = 3f;

//////    [Header("Hit")]
//////    [SerializeField] private LayerMask hitMask;
//////    [SerializeField] private int damage = 1;

//////    private float lifeTimer;

//////    private void Start()
//////    {
//////        lifeTimer = lifeTime;
//////    }

//////    private void Update()
//////    {
//////        transform.position += transform.forward * speed * Time.deltaTime;

//////        lifeTimer -= Time.deltaTime;
//////        if (lifeTimer <= 0f)
//////        {
//////            Destroy(gameObject);
//////        }
//////    }
//////}

////using UnityEngine;

////public class WeaponProjectile : MonoBehaviour
////{
////    [Header("Movement")]
////    [SerializeField] private float speed = 20f;
////    [SerializeField] private float lifeTime = 3f;

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

////        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
////        {
////            isDespawning = true;
////            Destroy(gameObject);
////        }
////    }
////}

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
//}

using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;

    [Header("Damage")]
    [SerializeField] private int damage = 1;

    [Header("Despawn")]
    [SerializeField] private LayerMask despawnMask;

    private float lifeTimer;
    private bool isDespawning;

    private void Start()
    {
        lifeTimer = lifeTime;
    }

    private void Update()
    {
        if (isDespawning)
            return;

        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDespawning)
            return;

        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyHealth = other.GetComponent<EnemyController>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        if ((despawnMask.value & (1 << other.gameObject.layer)) != 0)
        {
            isDespawning = true;
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}