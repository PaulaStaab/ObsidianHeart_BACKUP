////using UnityEngine;

////public class WeaponShoot : MonoBehaviour
////{
////    [SerializeField] public WeaponProjectile projectilePrefab;
////    [SerializeField] private Transform firePoint;

////    private void Update()
////    {
////        if (Input.GetMouseButtonDown(0))
////        {
////            Shoot();
////        }
////    }

////    private void Shoot()
////    {
////        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
////    }
////}

//using UnityEngine;

//public class WeaponShoot : MonoBehaviour
//{
//    [SerializeField] private GameObject projectilePrefab;
//    [SerializeField] private Transform firePoint;

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(1))
//        {
//            Shoot();
//        }
//    }

//    private void Shoot()
//    {
//        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
//    }
//}

using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Camera playerCamera;

    [Header("Aim")]
    [SerializeField] private float maxAimDistance = 100f;
    [SerializeField] private LayerMask aimMask;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (firePoint == null || projectilePrefab == null || playerCamera == null)
            return;

        Vector3 aimTargetPoint = GetAimTargetPoint();
        Vector3 shootDirection = (aimTargetPoint - firePoint.position).normalized;

        Quaternion projectileRotation = Quaternion.LookRotation(shootDirection);

        Instantiate(projectilePrefab, firePoint.position, projectileRotation);
    }

    private Vector3 GetAimTargetPoint()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance, aimMask, QueryTriggerInteraction.Ignore))
        {
            return hit.point;
        }

        return ray.GetPoint(maxAimDistance);
    }
}