//////////using UnityEngine;

//////////public class BulletDamage : MonoBehaviour
//////////{
//////////    public float damage = 100f;
//////////    public float speed = 20f;
//////////    public GameObject explosionPrefab;  // Ziehe deinen Explosion-Prefab hier rein

//////////    void Update()
//////////    {
//////////        transform.Translate(Vector3.forward * speed * Time.deltaTime);
//////////    }

//////////    private void OnTriggerEnter(Collider other)
//////////    {
//////////        if (other.CompareTag("Enemy"))
//////////        {
//////////            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
//////////            if (enemyHealth != null)
//////////            {
//////////                float healthBefore = enemyHealth.currentHealth;  // Aktuelle Health merken (muss public sein)
//////////                enemyHealth.TakeDamage(damage);

//////////                // Prüfen, ob Spinne durch diesen Schuss gestorben ist
//////////                if (healthBefore > 0 && enemyHealth.currentHealth <= 0)
//////////                {
//////////                    SpawnExplosion(other.transform.position);
//////////                }
//////////            }
//////////            else
//////////            {
//////////                Destroy(other.gameObject);
//////////                SpawnExplosion(other.transform.position);
//////////            }
//////////        }

//////////        Destroy(gameObject);
//////////    }

//////////    void SpawnExplosion(Vector3 position)
//////////    {
//////////        if (explosionPrefab != null)
//////////        {
//////////            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
//////////            // Optional: Zerstöre nach Dauer (falls nicht im Prefab-Skript)
//////////            Destroy(explosion, 2f);
//////////        }
//////////    }
//////////}

////////using UnityEngine;

////////public class BulletDamage : MonoBehaviour
////////{
////////    public float speed = 20f;
////////    public GameObject explosionPrefab;

////////    void Update()
////////    {
////////        transform.Translate(Vector3.forward * speed * Time.deltaTime);
////////    }

////////    private void OnTriggerEnter(Collider other)
////////    {
////////        if (other.CompareTag("Enemy"))
////////        {
////////            SpawnExplosion(other.transform.position);
////////            Destroy(other.gameObject);
////////        }

////////        Destroy(gameObject);
////////    }

////////    void SpawnExplosion(Vector3 position)
////////    {
////////        if (explosionPrefab != null)
////////        {
////////            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
////////            Destroy(explosion, 2f);
////////        }
////////    }
////////}

//////using System;
//////using UnityEngine;

//////public class BulletDamage : MonoBehaviour
//////{
//////    public float damage = 100f;
//////    public float speed = 20f;
//////    public GameObject explosionPrefab;

//////    void Update()
//////    {
//////        transform.Translate(Vector3.forward * speed * Time.deltaTime);
//////    }

//////    private void OnTriggerEnter(Collider other)
//////    {
//////        if (other.CompareTag("Enemy"))
//////        {
//////            bool enemyDied = false;

//////            // Beispiel: Enemy hat SpiderHealth
//////            EnemyController enemyHealth = other.GetComponent<EnemyController>();
//////            if (enemyHealth != null)
//////            {
//////                float healthBefore = enemyHealth.currentHealth;
//////                enemyHealth.TakeDamage(damage);

//////                if (healthBefore > 0 && enemyHealth.currentHealth <= 0)
//////                {
//////                    enemyDied = true;
//////                }
//////            }
//////            else
//////            {
//////                // Falls kein Health-Skript gefunden wurde, optional direkt zerstören
//////                Destroy(other.gameObject);
//////                enemyDied = true;
//////            }

//////            if (enemyDied)
//////            {
//////                SpawnExplosion(other.transform.position);
//////            }
//////        }

//////        Destroy(gameObject);
//////    }

//////    void SpawnExplosion(Vector3 position)
//////    {
//////        if (explosionPrefab != null)
//////        {
//////            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
//////            Destroy(explosion, 2f);
//////        }
//////    }

//////    internal int GetDamage()
//////    {
//////        throw new NotImplementedException();
//////    }
//////}

////using System;
////using UnityEngine;

////public class BulletDamage : MonoBehaviour
////{
////    public float damage = 100f;
////    public float speed = 20f;
////    public GameObject explosionPrefab;

////    void Update()
////    {
////        transform.Translate(Vector3.forward * speed * Time.deltaTime);
////    }

////    private void OnTriggerEnter(Collider other)
////    {
////        if (other.CompareTag("Enemy"))
////        {
////            bool enemyDied = false;

////            // Beispiel: Enemy hat SpiderHealth
////            EnemyController enemyHealth = other.GetComponent<EnemyController>();
////            if (enemyHealth != null)
////            {
////                float healthBefore = enemyHealth.currentHealth;
////                enemyHealth.TakeDamage(damage);

////                if (healthBefore > 0 && enemyHealth.currentHealth <= 0)
////                {
////                    enemyDied = true;
////                }
////            }
////            else
////            {
////                // Falls kein Health-Skript gefunden wurde, optional direkt zerstören
////                Destroy(other.gameObject);
////                enemyDied = true;
////            }

////            if (enemyDied)
////            {
////                SpawnExplosion(other.transform.position);
////            }
////        }

////        Destroy(gameObject);
////    }

////    void SpawnExplosion(Vector3 position)
////    {
////        if (explosionPrefab != null)
////        {
////            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
////            Destroy(explosion, 2f);
////        }
////    }

////    internal int GetDamage()
////    {
////        return Mathf.RoundToInt(damage);
////    }
////}

//using System;
//using UnityEngine;

//public class BulletDamage : MonoBehaviour
//{
//    public float damage = 100f;
//    public float speed = 20f;
//    public GameObject explosionPrefab;

//    void Update()
//    {
//        transform.Translate(Vector3.forward * speed * Time.deltaTime);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Enemy"))
//        {
//            bool enemyDied = false;

//            EnemyController enemyHealth = other.GetComponent<EnemyController>();
//            if (enemyHealth != null)
//            {
//                enemyDied = enemyHealth.TakeDamage(Mathf.RoundToInt(damage));
//            }
//            else
//            {
//                Destroy(other.gameObject);
//                enemyDied = true;
//            }

//            if (enemyDied)
//            {
//                SpawnExplosion(other.transform.position);
//            }
//        }

//        Destroy(gameObject);
//    }

//    void SpawnExplosion(Vector3 position)
//    {
//        if (explosionPrefab != null)
//        {
//            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
//            Destroy(explosion, 2f);
//        }
//    }

//    internal int GetDamage()
//    {
//        return Mathf.RoundToInt(damage);
//    }
//}

using System;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 100f;
    public float speed = 20f;

    [Header("Effects")]
    public GameObject explosionPrefab;
    public bool useTrail = false;

    private TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();

        if (trail != null)
        {
            trail.enabled = useTrail;
            trail.Clear();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            bool enemyDied = false;

            EnemyController enemyHealth = other.GetComponent<EnemyController>();
            if (enemyHealth != null)
            {
                enemyDied = enemyHealth.TakeDamage(Mathf.RoundToInt(damage));
            }
            else
            {
                Destroy(other.gameObject);
                enemyDied = true;
            }

            if (enemyDied)
            {
                SpawnExplosion(other.transform.position);
            }
        }

        Destroy(gameObject);
    }

    private void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }

    internal int GetDamage()
    {
        return Mathf.RoundToInt(damage);
    }
}