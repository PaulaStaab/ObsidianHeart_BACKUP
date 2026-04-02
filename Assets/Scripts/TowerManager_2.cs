using UnityEngine;

public class TowerManager_2 : MonoBehaviour
{

    [Header("Schießen - Kanone 1")]
    public GameObject bulletPrefab2;
    public Transform firePoint1;

    [Header("Schießen - Kanone 2")]
    public GameObject bulletPrefab3;
    public Transform firePoint2;

    [Header("Schießen - Kanone 3")]
    public GameObject bulletPrefab4;
    public Transform firePoint3;

    [Header("Allgemeine Schusswerte")]
    public float fireRate = 1f;
    public float range = 10f;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 10f;

    [Header("Start-Verzögerung")]
    public float delayBeforeFirstShot = 2f;

    [Header("Schusswinkel (Grad)")]
    [Range(-180f, 180f)] public float shootAngleX = 0f;
    [Range(-180f, 180f)] public float shootAngleY = 0f;
    [Range(-180f, 180f)] public float shootAngleZ = 0f;

    [Header("Rotation")]
    public bool rotateToTarget = true;
    public LayerMask enemyLayers = -1;

    private float nextFireTime = 0f;
    private float searchRadius = 5f;
    private bool hasStartedShooting = false;

    void Start()
    {
        nextFireTime = Time.time + delayBeforeFirstShot;
        hasStartedShooting = true;
    }

    void Update()
    {
        if (!hasStartedShooting) return;

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
                ShootAllCannons();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void ShootAllCannons()
    {
        ShootSingle(bulletPrefab2, firePoint1);
        ShootSingle(bulletPrefab3, firePoint2);
        ShootSingle(bulletPrefab4, firePoint3);
    }

    void ShootSingle(GameObject bulletPrefab, Transform firePoint)
    {
        // FIX: Prüft auch ob FirePoint aktiv ist!
        if (bulletPrefab == null || firePoint == null || !firePoint.gameObject.activeInHierarchy)
            return;

        Vector3 baseDirection = firePoint.forward;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 finalDirection = angleRot * baseDirection;
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
        DrawFirePointGizmo(firePoint1, Color.cyan);
        DrawFirePointGizmo(firePoint2, Color.green);
        DrawFirePointGizmo(firePoint3, Color.magenta);
    }

    void DrawFirePointGizmo(Transform firePoint, Color color)
    {
        if (firePoint == null) return;

        Gizmos.color = color;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 dir = (angleRot * firePoint.forward).normalized * range;
        Gizmos.DrawRay(firePoint.position, dir);
    }
}

