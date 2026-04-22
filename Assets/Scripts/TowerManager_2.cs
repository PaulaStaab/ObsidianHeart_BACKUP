using UnityEngine;

public class TowerManager_2 : MonoBehaviour
{
    // --- Shooting - Cannon 1 ---
    public GameObject bulletPrefab2;   // Prefab of the bullet fired by cannon 1
    public Transform firePoint1;       // Transform where cannon 1 shoots from

    // --- Shooting - Cannon 2 ---
    public GameObject bulletPrefab3;   // Prefab of the bullet fired by cannon 2
    public Transform firePoint2;       // Transform where cannon 2 shoots from

    // --- Shooting - Cannon 3 ---
    public GameObject bulletPrefab4;   // Prefab of the bullet fired by cannon 3
    public Transform firePoint3;       // Transform where cannon 3 shoots from

    // --- General shooting values ---
    public float fireRate = 1f;        // Shots per second
    public float range = 10f;          // Maximum shooting range
    public float bulletSpeed = 30f;    // Speed of the bullet
    public float bulletLifetime = 10f; // Time before bullet is destroyed

    // --- Delay before starting to shoot ---
    public float delayBeforeFirstShot = 2f; // Initial delay before first shot

    // --- Shooting angle offsets (in degrees) ---
    [Range(-180f, 180f)] public float shootAngleX = 0f; // Rotation offset on X axis
    [Range(-180f, 180f)] public float shootAngleY = 0f; // Rotation offset on Y axis
    [Range(-180f, 180f)] public float shootAngleZ = 0f; // Rotation offset on Z axis

    // --- Rotation behavior ---
    public bool rotateToTarget = true; // Should the tower rotate to face the target
    public LayerMask enemyLayers = -1; // Which layers count as enemies

    private float nextFireTime = 0f;   // Time when next shot is allowed
    private float searchRadius = 5f;   // Radius used to search for enemies
    private bool hasStartedShooting = false; // Whether shooting has started

    void Start()
    {
        // Set initial delay before the tower can shoot
        nextFireTime = Time.time + delayBeforeFirstShot;
        hasStartedShooting = true;
    }

    void Update()
    {
        // Do nothing if shooting hasn't started yet
        if (!hasStartedShooting) return;

        // Find all colliders within the search radius that match enemy layers
        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);

        Transform nearestTarget = null;
        float nearestDist = range;

        // Loop through all detected targets to find the closest one
        foreach (Collider col in targets)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearestTarget = col.transform;
            }
        }

        // If a target is found
        if (nearestTarget != null)
        {
            // Rotate tower to face the target if enabled
            if (rotateToTarget)
            {
                transform.LookAt(nearestTarget);
            }

            // Check if it's time to shoot
            if (Time.time >= nextFireTime)
            {
                ShootAllCannons();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void ShootAllCannons()
    {
        // Fire all three cannons
        ShootSingle(bulletPrefab2, firePoint1);
        ShootSingle(bulletPrefab3, firePoint2);
        ShootSingle(bulletPrefab4, firePoint3);
    }

    void ShootSingle(GameObject bulletPrefab, Transform firePoint)
    {
        // Safety check: ensure prefab and fire point exist and are active
        if (bulletPrefab == null || firePoint == null || !firePoint.gameObject.activeInHierarchy)
            return;

        // Base shooting direction (forward of fire point)
        Vector3 baseDirection = firePoint.forward;

        // Apply angle offset
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 finalDirection = angleRot * baseDirection;

        // Calculate bullet rotation to face shooting direction
        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

        // Instantiate the bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

        // Apply velocity to the bullet if it has a Rigidbody
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
        }

        // Destroy the bullet after its lifetime expires
        Destroy(bullet, bulletLifetime);
    }

    void OnDrawGizmosSelected()
    {
        // Draw direction rays for each fire point in the editor
        DrawFirePointGizmo(firePoint1, Color.cyan);
        DrawFirePointGizmo(firePoint2, Color.green);
        DrawFirePointGizmo(firePoint3, Color.magenta);
    }

    void DrawFirePointGizmo(Transform firePoint, Color color)
    {
        if (firePoint == null) return;

        Gizmos.color = color;

        // Apply same angle offset as shooting
        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
        Vector3 dir = (angleRot * firePoint.forward).normalized * range;

        // Draw a ray showing the shooting direction
        Gizmos.DrawRay(firePoint.position, dir);
    }
}