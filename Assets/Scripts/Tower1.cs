using UnityEngine;

public class Tower1 : MonoBehaviour
{
    [Header("Schieﬂen")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float range = 10f;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 10f;

    [Header("Schusswinkel (Grad)")]
    [Range(-180f, 180f)] public float shootAngleX = 0f;  // Pitch (hoch/runter)
    [Range(-180f, 180f)] public float shootAngleY = 0f;  // Yaw (links/rechts)  
    [Range(-180f, 180f)] public float shootAngleZ = 0f;  // Roll

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
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        // Richtung berechnen
        Vector3 baseDirection = firePoint != null ? firePoint.forward : transform.forward;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 finalDirection = angleRot * baseDirection;

        // Bullet-Rotation = Schussrichtung (Z nach vorne!)
        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.cyan;
        Vector3 startPos = firePoint != null ? firePoint.position : transform.position;
        Vector3 baseDir = firePoint != null ? firePoint.forward : transform.forward;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 dir = (angleRot * baseDir).normalized * range;
        Gizmos.DrawRay(startPos, dir);
    }
}

