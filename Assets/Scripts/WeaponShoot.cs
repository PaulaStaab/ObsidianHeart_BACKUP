using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform firePoint;        // Point where projectiles spawn
    [SerializeField] private GameObject projectilePrefab; // Projectile prefab to instantiate
    [SerializeField] private Camera playerCamera;        // Camera used for aiming
    [SerializeField] private AudioClip shootSound;       // Sound effect for shooting

    [Header("Aim")]
    [SerializeField] private float maxAimDistance = 100f; // Maximum aiming distance
    [SerializeField] private LayerMask aimMask;          // Layers that can be aimed at

    private AudioSource audioSource;                     // Audio component for playing shoot sound

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();       // Get AudioSource component
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))                 // Right mouse button pressed
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (firePoint == null || projectilePrefab == null || playerCamera == null) // Safety check
            return;

        Vector3 aimTargetPoint = GetAimTargetPoint();    // Get where player is aiming
        Vector3 shootDirection = (aimTargetPoint - firePoint.position).normalized; // Direction to shoot

        Quaternion projectileRotation = Quaternion.LookRotation(shootDirection); // Rotate projectile toward target

        Instantiate(projectilePrefab, firePoint.position, projectileRotation); // Spawn projectile

        if (audioSource != null && shootSound != null)   // Play shooting sound if available
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private Vector3 GetAimTargetPoint()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // Ray from center of screen

        if (Physics.Raycast(ray, out RaycastHit hit, maxAimDistance, aimMask, QueryTriggerInteraction.Ignore)) // Hit something
        {
            return hit.point;                            // Return hit point
        }

        return ray.GetPoint(maxAimDistance);             // Return max distance point if nothing hit
    }
}