using UnityEngine;

// Controls a tower with up to three cannons that search for nearby enemies,
// optionally rotate toward a target, and fire projectiles at a fixed rate.
public class Tower1 : MonoBehaviour
{
    [Header("Schieﬂen - Kanone 1")]
    // Bullet prefab used by cannon 1.
    public GameObject bulletPrefab1;
    // Fire point used by cannon 1.
    public Transform firePoint1;

    [Header("Schieﬂen - Kanone 2")]
    // Bullet prefab used by cannon 2.
    public GameObject bulletPrefab2;
    // Fire point used by cannon 2.
    public Transform firePoint2;

    [Header("Schieﬂen - Kanone 3")]
    // Bullet prefab used by cannon 3.
    public GameObject bulletPrefab3;
    // Fire point used by cannon 3.
    public Transform firePoint3;

    [Header("Allgemeine Schusswerte")]
    // Number of shots per second.
    public float fireRate = 1f;
    // Maximum attack range for target selection and gizmos.
    public float range = 10f;
    // Speed applied to fired bullets.
    public float bulletSpeed = 30f;
    // Lifetime of each spawned bullet.
    public float bulletLifetime = 10f;

    [Header("Start-Verzˆgerung")]
    // Delay before the tower is allowed to fire for the first time.
    public float delayBeforeFirstShot = 2f;

    [Header("Schusswinkel (Grad)")]
    // Additional angle offset for projectile direction on the X axis.
    [Range(-180f, 180f)] public float shootAngleX = 0f;
    // Additional angle offset for projectile direction on the Y axis.
    [Range(-180f, 180f)] public float shootAngleY = 0f;
    // Additional angle offset for projectile direction on the Z axis.
    [Range(-180f, 180f)] public float shootAngleZ = 0f;

    [Header("Rotation")]
    // If true, the tower rotates to face the current target.
    public bool rotateToTarget = true;
    // Layer mask used to search for enemies.
    public LayerMask enemyLayers = -1;

    // Time when the tower is next allowed to fire.
    private float nextFireTime = 0f;
    // Radius used to search for nearby enemies.
    private float searchRadius = 5f;
    // Becomes true after the initial setup so shooting can begin.
    private bool hasStartedShooting = false;

    void Start()
    {
        // Set the first allowed shot time after the configured delay.
        nextFireTime = Time.time + delayBeforeFirstShot;
        hasStartedShooting = true;
    }

    void Update()
    {
        // Stop if shooting has not started yet.
        if (!hasStartedShooting) return;

        // Find all potential enemy targets within the search radius.
        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);
        Transform nearestTarget = null;
        float nearestDist = range;

        // Select the nearest target that is still within the configured range.
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
                // Rotate the tower to face the selected target.
                transform.LookAt(nearestTarget);
            }

            if (Time.time >= nextFireTime)
            {
                // Fire from all configured cannons and schedule the next shot.
                ShootAllCannons();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void ShootAllCannons()
    {
        // Fire one shot from each available cannon.
        ShootSingle(bulletPrefab1, firePoint1);
        ShootSingle(bulletPrefab2, firePoint2);
        ShootSingle(bulletPrefab3, firePoint3);
    }

    void ShootSingle(GameObject bulletPrefab, Transform firePoint)
    {
        // Skip shooting if the prefab or fire point is missing,
        // or if the fire point is currently inactive.
        if (bulletPrefab == null || firePoint == null || !firePoint.gameObject.activeInHierarchy)
            return;

        // Build the projectile direction using the fire point forward direction
        // plus the configured angle offset.
        Vector3 baseDirection = firePoint.forward;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 finalDirection = angleRot * baseDirection;
        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

        // Spawn the projectile at the fire point.
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        // Apply velocity if the projectile has a Rigidbody.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
        }

        // Destroy the projectile automatically after its lifetime expires.
        Destroy(bullet, bulletLifetime);
    }

    void OnDrawGizmosSelected()
    {
        // Draw debug rays for all three cannon fire points.
        DrawFirePointGizmo(firePoint1, Color.cyan);
        DrawFirePointGizmo(firePoint2, Color.green);
        DrawFirePointGizmo(firePoint3, Color.magenta);
    }

    void DrawFirePointGizmo(Transform firePoint, Color color)
    {
        // Skip drawing if the fire point is missing.
        if (firePoint == null) return;

        // Draw the final shot direction in the Scene view.
        Gizmos.color = color;
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 dir = (angleRot * firePoint.forward).normalized * range;
        Gizmos.DrawRay(firePoint.position, dir);
    }
}