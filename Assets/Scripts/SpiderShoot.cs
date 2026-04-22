using UnityEngine;

// Handles periodic spider shooting once shooting has been unlocked by trigger zones.
public class SpiderShoot : MonoBehaviour
{
    [Header("Bullet")]
    // Bullet prefab that will be instantiated when the spider shoots.
    [SerializeField] private GameObject bulletPrefab;
    // Spawn point and rotation used for the projectile.
    [SerializeField] private Transform shootPoint;

    [Header("Timing")]
    // Time in seconds between shots.
    [SerializeField] private float shootInterval = 2f;

    [Header("Sound")]
    // Optional sound played whenever a shot is fired.
    [SerializeField] private AudioClip shootSound;

    [Header("Debug")]
    // Global switch to allow or block shooting behavior.
    [SerializeField] private bool canShoot = true;

    // Tracks elapsed time since the last shot.
    private float timer = 0f;
    // Becomes true once the spider has entered one of the shooting zones.
    private bool shootingUnlocked = false;
    // Cached AudioSource used to play the shooting sound.
    private AudioSource audioSource;

    private void Awake()
    {
        // Cache the AudioSource component on this object.
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Stop early if shooting is disabled, not yet unlocked, or required references are missing.
        if (!canShoot) return;
        if (!shootingUnlocked) return;
        if (bulletPrefab == null || shootPoint == null) return;

        // Count up the timer until the next shot is ready.
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        // Spawn the projectile at the configured shoot point.
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Play the shooting sound if both clip and audio source are available.
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Permanently unlock shooting after entering one of the configured trigger zones.
        if (other.name == "shootingZone(1)" ||
            other.name == "shootingZone(2)" ||
            other.name == "shootingZone(3)")
        {
            shootingUnlocked = true;
            Debug.Log("Shooting permanently activated by: " + other.name);
        }
    }
}