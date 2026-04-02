//////using UnityEngine;

//////public class Tower1 : MonoBehaviour
//////{
//////    [Header("Schießen")]
//////    public GameObject bulletPrefab;
//////    public Transform firePoint;
//////    public float fireRate = 1f;
//////    public float range = 10f;
//////    public float bulletSpeed = 30f;
//////    public float bulletLifetime = 10f;

//////    [Header("Schusswinkel (Grad)")]
//////    [Range(-180f, 180f)] public float shootAngleX = 0f;  // Pitch (hoch/runter)
//////    [Range(-180f, 180f)] public float shootAngleY = 0f;  // Yaw (links/rechts)  
//////    [Range(-180f, 180f)] public float shootAngleZ = 0f;  // Roll

//////    [Header("Rotation")]
//////    public bool rotateToTarget = true;
//////    public LayerMask enemyLayers = -1;

//////    private float nextFireTime = 0f;
//////    private float searchRadius = 5f;

//////    void Update()
//////    {
//////        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);
//////        Transform nearestTarget = null;
//////        float nearestDist = range;

//////        foreach (Collider col in targets)
//////        {
//////            float dist = Vector3.Distance(transform.position, col.transform.position);
//////            if (dist < nearestDist)
//////            {
//////                nearestDist = dist;
//////                nearestTarget = col.transform;
//////            }
//////        }

//////        if (nearestTarget != null)
//////        {
//////            if (rotateToTarget)
//////            {
//////                transform.LookAt(nearestTarget);
//////            }

//////            if (Time.time >= nextFireTime)
//////            {
//////                Shoot();
//////                nextFireTime = Time.time + 1f / fireRate;
//////            }
//////        }
//////    }

//////    void Shoot()
//////    {
//////        if (bulletPrefab == null || firePoint == null) return;

//////        // Richtung berechnen
//////        Vector3 baseDirection = firePoint != null ? firePoint.forward : transform.forward;
//////        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
//////        Vector3 finalDirection = angleRot * baseDirection;

//////        // Bullet-Rotation = Schussrichtung (Z nach vorne!)
//////        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

//////        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

//////        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//////        if (rb != null)
//////        {
//////            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
//////        }

//////        Destroy(bullet, bulletLifetime);
//////    }


//////    void OnDrawGizmosSelected()
//////    {
//////        Gizmos.color = Color.red;
//////        Gizmos.DrawWireSphere(transform.position, searchRadius);
//////        Gizmos.color = Color.yellow;
//////        Gizmos.DrawWireSphere(transform.position, range);

//////        Gizmos.color = Color.cyan;
//////        Vector3 startPos = firePoint != null ? firePoint.position : transform.position;
//////        Vector3 baseDir = firePoint != null ? firePoint.forward : transform.forward;
//////        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
//////        Vector3 dir = (angleRot * baseDir).normalized * range;
//////        Gizmos.DrawRay(startPos, dir);
//////    }
//////}

////using UnityEngine;

////public class Tower1 : MonoBehaviour
////{
////    [Header("Schießen")]
////    public GameObject bulletPrefab;
////    public Transform firePoint;
////    public float fireRate = 1f;
////    public float range = 10f;
////    public float bulletSpeed = 30f;
////    public float bulletLifetime = 10f;

////    [Header("Start-Verzögerung")]
////    public float delayBeforeFirstShot = 2f;  

////    [Header("Schusswinkel (Grad)")]
////    [Range(-180f, 180f)] public float shootAngleX = 0f;
////    [Range(-180f, 180f)] public float shootAngleY = 0f;
////    [Range(-180f, 180f)] public float shootAngleZ = 0f;

////    [Header("Rotation")]
////    public bool rotateToTarget = true;
////    public LayerMask enemyLayers = -1;

////    private float nextFireTime = 0f;
////    private float searchRadius = 5f;
////    private bool hasStartedShooting = false;  // Flag für Start-Verzögerung

////    void Start()
////    {
////        // nextFireTime auf delayBeforeFirstShot setzen
////        nextFireTime = Time.time + delayBeforeFirstShot;
////        hasStartedShooting = true;
////    }

////    void Update()
////    {
////        if (!hasStartedShooting) return;  // Frühstopp vor Start

////        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);
////        Transform nearestTarget = null;
////        float nearestDist = range;

////        foreach (Collider col in targets)
////        {
////            float dist = Vector3.Distance(transform.position, col.transform.position);
////            if (dist < nearestDist)
////            {
////                nearestDist = dist;
////                nearestTarget = col.transform;
////            }
////        }

////        if (nearestTarget != null)
////        {
////            if (rotateToTarget)
////            {
////                transform.LookAt(nearestTarget);
////            }

////            if (Time.time >= nextFireTime)
////            {
////                Shoot();
////                nextFireTime = Time.time + 1f / fireRate;
////            }
////        }
////    }

////    void Shoot()
////    {
////        if (bulletPrefab == null || firePoint == null) return;

////        Vector3 baseDirection = firePoint != null ? firePoint.forward : transform.forward;
////        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
////        Vector3 finalDirection = angleRot * baseDirection;
////        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

////        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

////        Rigidbody rb = bullet.GetComponent<Rigidbody>();
////        if (rb != null)
////        {
////            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
////        }
////        // Destroy(bullet, bulletLifetime);  // Bleibt auskommentiert (Bullet-Skript übernimmt)
////    }

////    void OnDrawGizmosSelected()
////    {
////        Gizmos.color = Color.red;
////        Gizmos.DrawWireSphere(transform.position, searchRadius);
////        Gizmos.color = Color.yellow;
////        Gizmos.DrawWireSphere(transform.position, range);

////        Gizmos.color = Color.cyan;
////        Vector3 startPos = firePoint != null ? firePoint.position : transform.position;
////        Vector3 baseDir = firePoint != null ? firePoint.forward : transform.forward;
////        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
////        Vector3 dir = (angleRot * baseDir).normalized * range;
////        Gizmos.DrawRay(startPos, dir);
////    }
////}


//using UnityEngine;

//public class Tower1 : MonoBehaviour
//{
//    [Header("Schießen - Kanone 1")]
//    public GameObject bulletPrefab1;
//    public Transform firePoint1;

//    [Header("Schießen - Kanone 2")]
//    public GameObject bulletPrefab2;
//    public Transform firePoint2;

//    [Header("Schießen - Kanone 3")]
//    public GameObject bulletPrefab3;
//    public Transform firePoint3;

//    [Header("Allgemeine Schusswerte")]
//    public float fireRate = 1f;
//    public float range = 10f;
//    public float bulletSpeed = 30f;
//    public float bulletLifetime = 10f;

//    [Header("Start-Verzögerung")]
//    public float delayBeforeFirstShot = 2f;

//    [Header("Schusswinkel (Grad)")]
//    [Range(-180f, 180f)] public float shootAngleX = 0f;
//    [Range(-180f, 180f)] public float shootAngleY = 0f;
//    [Range(-180f, 180f)] public float shootAngleZ = 0f;

//    [Header("Rotation")]
//    public bool rotateToTarget = true;
//    public LayerMask enemyLayers = -1;

//    private float nextFireTime = 0f;
//    private float searchRadius = 5f;
//    private bool hasStartedShooting = false;

//    void Start()
//    {
//        nextFireTime = Time.time + delayBeforeFirstShot;
//        hasStartedShooting = true;
//    }

//    void Update()
//    {
//        if (!hasStartedShooting) return;

//        Collider[] targets = Physics.OverlapSphere(transform.position, searchRadius, enemyLayers);
//        Transform nearestTarget = null;
//        float nearestDist = range;

//        foreach (Collider col in targets)
//        {
//            float dist = Vector3.Distance(transform.position, col.transform.position);
//            if (dist < nearestDist)
//            {
//                nearestDist = dist;
//                nearestTarget = col.transform;
//            }
//        }

//        if (nearestTarget != null)
//        {
//            if (rotateToTarget)
//            {
//                transform.LookAt(nearestTarget);
//            }

//            if (Time.time >= nextFireTime)
//            {
//                ShootAllCannons();
//                nextFireTime = Time.time + 1f / fireRate;
//            }
//        }
//    }

//    void ShootAllCannons()
//    {
//        ShootSingle(bulletPrefab1, firePoint1);
//        ShootSingle(bulletPrefab2, firePoint2);
//        ShootSingle(bulletPrefab3, firePoint3);
//    }

//    void ShootSingle(GameObject bulletPrefab, Transform firePoint)
//    {
//        if (bulletPrefab == null || firePoint == null) return;

//        Vector3 baseDirection = firePoint.forward;
//        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
//        Vector3 finalDirection = angleRot * baseDirection;
//        Quaternion bulletRotation = Quaternion.LookRotation(finalDirection);

//        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

//        Rigidbody rb = bullet.GetComponent<Rigidbody>();
//        if (rb != null)
//        {
//            rb.linearVelocity = finalDirection.normalized * bulletSpeed;
//        }

//        Destroy(bullet, bulletLifetime);
//    }

//    void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, searchRadius);

//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, range);

//        DrawFirePointGizmo(firePoint1, Color.cyan);
//        DrawFirePointGizmo(firePoint2, Color.green);
//        DrawFirePointGizmo(firePoint3, Color.magenta);
//    }

//    void DrawFirePointGizmo(Transform firePoint, Color color)
//    {
//        if (firePoint == null) return;

//        Gizmos.color = color;
//        Quaternion angleRot = Quaternion.Euler(shootAngleX, shootAngleY, shootAngleZ);
//        Vector3 dir = (angleRot * firePoint.forward).normalized * range;
//        Gizmos.DrawRay(firePoint.position, dir);
//    }
//}

using UnityEngine;

public class Tower1 : MonoBehaviour
{
    [Header("Schießen - Kanone 1")]
    public GameObject bulletPrefab1;
    public Transform firePoint1;

    [Header("Schießen - Kanone 2")]
    public GameObject bulletPrefab2;
    public Transform firePoint2;

    [Header("Schießen - Kanone 3")]
    public GameObject bulletPrefab3;
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
        ShootSingle(bulletPrefab1, firePoint1);
        ShootSingle(bulletPrefab2, firePoint2);
        ShootSingle(bulletPrefab3, firePoint3);
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