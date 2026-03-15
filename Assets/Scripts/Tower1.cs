using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Schießen")]
    public GameObject bulletPrefab; // Dein bestehendes Bullet-Prefab zuweisen
    public Transform firePoint; // Mündung
    public float fireRate = 1f;
    public float range = 10f;

    [Header("Rotation")]
    public bool rotateToTarget = true;
    public LayerMask enemyLayers = -1;

    private float nextFireTime = 0f;
    private float searchRadius = 5f;

    void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);
        Transform nearestTarget = null;
        float nearestDist = range;

        foreach (Collider col in targets)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearestTarget = col.transform;
            }
        }

        if (nearestTarget != null)
        {
            if (rotateToTarget)
            {
                transform.LookAt(nearestTarget);
            }

            if (Time.time >= nextFireTime)
            {
                Shoot(nearestTarget);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot(Transform target)
    {
        if (bulletPrefab == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 dir = (target.position - firePoint.position).normalized;
        
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = dir * -30f; // Richtet und schießt deine Bullet
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
