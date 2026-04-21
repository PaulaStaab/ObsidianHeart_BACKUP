
using UnityEngine;

public class MonsterShoot : MonoBehaviour
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