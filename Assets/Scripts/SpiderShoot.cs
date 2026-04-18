////using UnityEngine;

////public class SpiderShoot : MonoBehaviour
////{
////    [Header("Bullet")]
////    [SerializeField] private GameObject bulletPrefab;
////    [SerializeField] private Transform shootPoint;

////    [Header("Timing")]
////    [SerializeField] private float shootInterval = 2f;

////    [Header("Debug")]
////    [SerializeField] private bool canShoot = true;

////    private float timer = 0f;

////    private void Update()
////    {
////        if (!canShoot) return;
////        if (bulletPrefab == null || shootPoint == null) return;

////        timer += Time.deltaTime;

////        if (timer >= shootInterval)
////        {
////            Shoot();
////            timer = 0f;
////        }
////    }

////    private void Shoot()
////    {
////        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
////    }
////}

//using UnityEngine;

//public class SpiderShoot : MonoBehaviour
//{
//    [Header("Bullet")]
//    [SerializeField] private GameObject bulletPrefab;
//    [SerializeField] private Transform shootPoint;

//    [Header("Shooting Zones")]
//    [SerializeField] private Collider shootingZone1;
//    [SerializeField] private Collider shootingZone2;
//    [SerializeField] private Collider shootingZone3;

//    [Header("Timing")]
//    [SerializeField] private float shootInterval = 2f;

//    [Header("Debug")]
//    [SerializeField] private bool canShoot = true;

//    private float timer = 0f;
//    private bool hasEnteredShootingZone = false;

//    private void Update()
//    {
//        if (!canShoot) return;
//        if (!hasEnteredShootingZone) return;
//        if (bulletPrefab == null || shootPoint == null) return;

//        timer += Time.deltaTime;

//        if (timer >= shootInterval)
//        {
//            Shoot();
//            timer = 0f;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other == shootingZone1 || other == shootingZone2 || other == shootingZone3)
//        {
//            hasEnteredShootingZone = true;
//        }
//    }

//    private void Shoot()
//    {
//        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
//    }
//}

using UnityEngine;

public class SpiderShoot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Timing")]
    [SerializeField] private float shootInterval = 2f;

    [Header("Test")]
    [SerializeField] private bool testShoot = false;

    [Header("Debug")]
    [SerializeField] private bool canShoot = true;

    private float timer = 0f;
    private bool isInShootingZone = false;

    private Collider shootingZone1;
    private Collider shootingZone2;
    private Collider shootingZone3;

    private void Start()
    {
        GameObject zoneObj1 = GameObject.Find("shootingZone(1)");
        GameObject zoneObj2 = GameObject.Find("shootingZone(2)");
        GameObject zoneObj3 = GameObject.Find("shootingZone(3)");

        if (zoneObj1 != null) shootingZone1 = zoneObj1.GetComponent<Collider>();
        if (zoneObj2 != null) shootingZone2 = zoneObj2.GetComponent<Collider>();
        if (zoneObj3 != null) shootingZone3 = zoneObj3.GetComponent<Collider>();
    }

    private void Update()
    {
        if (!canShoot) return;
        if (bulletPrefab == null || shootPoint == null) return;

        bool shootEnabled = testShoot || isInShootingZone;
        if (!shootEnabled) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == shootingZone1 || other == shootingZone2 || other == shootingZone3)
        {
            isInShootingZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == shootingZone1 || other == shootingZone2 || other == shootingZone3)
        {
            isInShootingZone = false;
            timer = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}