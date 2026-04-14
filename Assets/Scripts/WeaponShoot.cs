//using UnityEngine;

//public class WeaponShoot : MonoBehaviour
//{
//    [SerializeField] public WeaponProjectile projectilePrefab;
//    [SerializeField] private Transform firePoint;

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Shoot();
//        }
//    }

//    private void Shoot()
//    {
//        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
//    }
//}

using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}