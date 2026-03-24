//using UnityEngine;

//public class SpiderHealth : MonoBehaviour
//{
//    public int maxHealth = 10;
//    public int currentHealth;

//    void Start()
//    {
//        currentHealth = maxHealth;
//    }

//    public void TakeDamage(int damage)
//    {
//        currentHealth -= damage;

//        if (currentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class SpiderHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Optional: Effekte, Score-Update, etc.
        Destroy(gameObject);
    }

    // Für Trigger-Kollisionen (empfohlen für Bullets)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))  // Tag "Bullet" auf Bullet-Objekten setzen
        {
            BulletDamage bullet = other.GetComponent<BulletDamage>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);  // Damage aus Bullet-Skript
            }
            Destroy(other.gameObject);  // Bullet zerstören
        }
    }
}

