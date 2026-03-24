using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 100f;  // Schaden (für sofortigen Tod: = maxHealth der Spinne)
    public float speed = 20f;

    void Update()
    {
        // Vorwärtsbewegung
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Über Tag "Spinne" erkennen
        if (other.CompareTag("Spinne"))
        {
            // Schaden an Spinne-Skript übertragen (aus vorherigem EnemyHealth)
            SpiderHealth enemyHealth = other.GetComponent<SpiderHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // Verursacht Tod bei damage >= Health
            }
            else
            {
                // Fallback: Sofort despawnen
                Destroy(other.gameObject);
            }
        }

        // Bullet immer despawnen
        Destroy(gameObject);
    }
}
