using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 100f;
    public float speed = 20f;

    [Header("Explosion")]
    public GameObject explosionPrefab;   // Bleibt für Treffer/Explosion

    [Header("Trail / Schweif")]
    public GameObject trailEffectPrefab; // Dein vorhandener Effekt
    public Vector3 trailLocalOffset = new Vector3(0f, 0f, -0.5f);
    public float detachedTrailLifetime = 1.5f;

    private GameObject spawnedTrail;

    void Start()
    {
        if (trailEffectPrefab != null)
        {
            spawnedTrail = Instantiate(trailEffectPrefab, transform);
            spawnedTrail.transform.localPosition = trailLocalOffset;
            spawnedTrail.transform.localRotation = Quaternion.identity;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
            if (enemyHealth != null)
            {
                float healthBefore = enemyHealth.currentHealth;
                enemyHealth.TakeDamage(damage);

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

        DetachTrail();
        Destroy(gameObject);
    }

    void SpawnExplosion(Vector3 position)
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }

    void DetachTrail()
    {
        if (spawnedTrail == null)
            return;

        spawnedTrail.transform.parent = null;

        ParticleSystem[] particleSystems = spawnedTrail.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        Destroy(spawnedTrail, detachedTrailLifetime);
        spawnedTrail = null;
    }
}