////using UnityEngine;

////public class EnemyController : MonoBehaviour
////{
////    [Header("Health Settings")]
////    [SerializeField] private int maxHealth = 100;

////    private int currentHealth;

////    private void Awake()
////    {
////        currentHealth = maxHealth;
////    }

////    public void TakeDamage(int damage)
////    {
////        currentHealth -= damage;

////        if (currentHealth <= 0)
////        {
////            Die();
////        }
////    }

////    private void Die()
////    {
////        // Hier kannst du später Tod-Animation, Drops, Sound usw. einbauen.
////        Destroy(gameObject);
////    }

////    // Optional, falls du im Code HP ändern willst
////    public void SetMaxHealth(int newMaxHealth)
////    {
////        maxHealth = newMaxHealth;
////        currentHealth = maxHealth;
////    }

////    public int CurrentHealth => currentHealth;
////    public int MaxHealth => maxHealth;
////}

//using System;
//using UnityEngine;

//public class EnemyController : MonoBehaviour
//{
//    [Header("Health")]
//    [SerializeField] private int maxHealth = 100;

//    [Header("Damage from Tags")]
//    [SerializeField] private string playerProjectileTag = "Shoot";
//    [SerializeField] private string towerBulletTag = "Bullet";

//    public int currentHealth;

//    public void Awake()
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

//    private void Die()
//    {
//        Destroy(gameObject);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag(playerProjectileTag))
//        {
//            WeaponProjectile projectile = other.GetComponent<WeaponProjectile>();

//            if (projectile != null)
//            {
//                TakeDamage(projectile.GetDamage());
//            }

//            return;
//        }

//        if (other.CompareTag(towerBulletTag))
//        {
//            BulletDamage bullet = other.GetComponent<BulletDamage>();

//            if (bullet != null)
//            {
//                TakeDamage(bullet.GetDamage());
//            }

//            return;
//        }
//    }

//    public int GetCurrentHealth()
//    {
//        return currentHealth;
//    }

//    public int GetMaxHealth()
//    {
//        return maxHealth;
//    }

//    internal void TakeDamage(float damage)
//    {
//        throw new NotImplementedException();
//    }
//}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("Damage from Tags")]
    [SerializeField] private string playerProjectileTag = "Shoot";
    [SerializeField] private string towerBulletTag = "Bullet";

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}