//////using UnityEngine;

//////public class SpiderShoot : MonoBehaviour
//////{
//////    [Header("Bullet")]
//////    [SerializeField] private GameObject bulletPrefab;
//////    [SerializeField] private Transform shootPoint;

//////    [Header("Timing")]
//////    [SerializeField] private float shootInterval = 2f;

//////    [Header("Debug")]
//////    [SerializeField] private bool canShoot = true;

//////    private float timer = 0f;

//////    private void Update()
//////    {
//////        if (!canShoot) return;
//////        if (bulletPrefab == null || shootPoint == null) return;

//////        timer += Time.deltaTime;

//////        if (timer >= shootInterval)
//////        {
//////            Shoot();
//////            timer = 0f;
//////        }
//////    }

//////    private void Shoot()
//////    {
//////        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
//////    }
//////}

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

////    private GameObject shootingZone1;
////    private GameObject shootingZone2;
////    private GameObject shootingZone3;

////    private float timer = 0f;
////    private bool isInShootingZone = false;

////    private void Start()
////    {
////        shootingZone1 = GameObject.Find("shootingZone(1)");
////        shootingZone2 = GameObject.Find("shootingZone(2)");
////        shootingZone3 = GameObject.Find("shootingZone(3)");

////        if (shootingZone1 == null) Debug.LogWarning("shootingZone(1) nicht gefunden!");
////        if (shootingZone2 == null) Debug.LogWarning("shootingZone(2) nicht gefunden!");
////        if (shootingZone3 == null) Debug.LogWarning("shootingZone(3) nicht gefunden!");
////    }

////    private void Update()
////    {
////        if (!canShoot) return;
////        if (!isInShootingZone) return;
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

////    private void OnTriggerEnter(Collider other)
////    {
////        if (other.gameObject == shootingZone1 ||
////            other.gameObject == shootingZone2 ||
////            other.gameObject == shootingZone3)
////        {
////            isInShootingZone = true;
////            timer = 0f;
////        }
////    }

////    private void OnTriggerExit(Collider other)
////    {
////        if (other.gameObject == shootingZone1 ||
////            other.gameObject == shootingZone2 ||
////            other.gameObject == shootingZone3)
////        {
////            isInShootingZone = false;
////            timer = 0f;
////        }
////    }
////}

//using UnityEngine;

//public class SpiderShoot : MonoBehaviour
//{
//    [Header("Bullet")]
//    [SerializeField] private GameObject bulletPrefab;
//    [SerializeField] private Transform shootPoint;

//    [Header("Timing")]
//    [SerializeField] private float shootInterval = 2f;

//    [Header("Debug")]
//    [SerializeField] private bool canShoot = true;

//    private float timer = 0f;
//    private bool shootingUnlocked = false;

//    private void Update()
//    {
//        if (!canShoot) return;
//        if (!shootingUnlocked) return;
//        if (bulletPrefab == null || shootPoint == null) return;

//        timer += Time.deltaTime;

//        if (timer >= shootInterval)
//        {
//            Shoot();
//            timer = 0f;
//        }
//    }

//    private void Shoot()
//    {
//        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.name == "shootingZone(1)" ||
//            other.name == "shootingZone(2)" ||
//            other.name == "shootingZone(3)")
//        {
//            shootingUnlocked = true;
//            Debug.Log("Shooting dauerhaft aktiviert durch: " + other.name);
//        }
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

    [Header("Sound")]
    [SerializeField] private AudioClip shootSound;

    [Header("Debug")]
    [SerializeField] private bool canShoot = true;

    private float timer = 0f;
    private bool shootingUnlocked = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!canShoot) return;
        if (!shootingUnlocked) return;
        if (bulletPrefab == null || shootPoint == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "shootingZone(1)" ||
            other.name == "shootingZone(2)" ||
            other.name == "shootingZone(3)")
        {
            shootingUnlocked = true;
            Debug.Log("Shooting dauerhaft aktiviert durch: " + other.name);
        }
    }
}