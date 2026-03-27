using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 100f;
    public float speed = 20f;
    public GameObject explosionPrefab;  // Ziehe deinen Explosion-Prefab hier rein

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spinne"))
        {
            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
            if (enemyHealth != null)
            {
                float healthBefore = enemyHealth.currentHealth;  // Aktuelle Health merken (muss public sein)
                enemyHealth.TakeDamage(damage);

                // Prüfen, ob Spinne durch diesen Schuss gestorben ist
                if (healthBefore > 0 && enemyHealth.currentHealth <= 0)
                {
                    SpawnExplosion(other.transform.position);
                }
            }
            else
            {
                Destroy(other.gameObject);
                SpawnExplosion(other.transform.position);
            }
        }

        Destroy(gameObject);
    }

    void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            // Optional: Zerstöre nach Dauer (falls nicht im Prefab-Skript)
            Destroy(explosion, 2f);
        }
    }
}

