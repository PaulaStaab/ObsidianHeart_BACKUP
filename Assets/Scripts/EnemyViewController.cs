using UnityEngine;

// Controls enemy vision, patrolling, chasing, aiming, and shooting behavior.
public class EnemyViewController : MonoBehaviour
{
    [Header("References")]
    // Reference to the player transform.
    [SerializeField] private Transform player;
    // Point from which line of sight checks are performed.
    [SerializeField] private Transform eyePoint;
    // Point from which bullets are spawned.
    [SerializeField] private Transform firePoint;

    [Header("Movement")]
    // Movement speed while patrolling.
    [SerializeField] private float patrolSpeed = 3f;
    // Movement speed while chasing the player.
    [SerializeField] private float chaseSpeed = 4.5f;
    // Distance at which the enemy stops moving closer.
    [SerializeField] private float stopDistance = 1.5f;
    // Rotation smoothing speed.
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Patrol Points")]
    // List of waypoints used for patrol movement.
    [SerializeField] private Transform[] waypoints;
    // Index of the current patrol target.
    private int currentWaypointIndex = 0;

    [Header("Vision")]
    // Maximum distance at which the enemy can see the player.
    [SerializeField] private float viewDistance = 15f;
    // Field of view angle in degrees.
    [SerializeField, Range(0f, 180f)] private float viewAngle = 90f;
    // Radius used for the sphere cast to make detection more forgiving.
    [SerializeField] private float sphereCastRadius = 0.35f;

    [Header("Shooting")]
    // Bullet prefab fired by the enemy.
    [SerializeField] private GameObject bulletPrefab;
    // Delay between shots.
    [SerializeField] private float shootCooldown = 1.2f;
    // Speed applied to the spawned bullet.
    [SerializeField] private float bulletSpeed = 20f;
    // Optional sound effect played when shooting.
    [SerializeField] private AudioClip shootSound;
    // Internal timer used to control fire rate.
    private float shootTimer = 0f;

    [Header("Layers")]
    // Layer mask used for visibility checks.
    [SerializeField] private LayerMask detectionMask;

    [Header("Debug")]
    // Enables log output when the player gets detected.
    [SerializeField] private bool debugLogDetection = true;
    // Enables scene gizmos for vision debugging.
    [SerializeField] private bool debugDrawGizmos = true;

    // True while the enemy currently sees the player.
    public bool CanSeePlayer { get; private set; }
    // Stores the player's last seen position.
    public Vector3 LastKnownPlayerPosition { get; private set; }

    // Tracks the previous frame's detection state for one-time logging.
    private bool wasSeeingPlayerLastFrame = false;
    // Cached audio source for shooting sounds.
    private AudioSource audioSource;
    // Cached player health reference.
    private PlayerHealth playerHealth;

    public void SetPlayer(Transform playerTransform)
    {
        // Assign the player reference from another script.
        player = playerTransform;

        if (player != null)
        {
            // Try to get the player's health component directly or from a parent object.
            playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                playerHealth = player.GetComponentInParent<PlayerHealth>();
            }
        }
    }

    private void Awake()
    {
        // Cache the AudioSource on this enemy.
        audioSource = GetComponent<AudioSource>();

        if (player != null)
        {
            // Initialize the player health reference if the player is already assigned.
            playerHealth = player.GetComponent<PlayerHealth>();

            if (playerHealth == null)
            {
                playerHealth = player.GetComponentInParent<PlayerHealth>();
            }
        }
    }

    private void Update()
    {
        // Count down the shooting cooldown timer every frame.
        shootTimer -= Time.deltaTime;

        // If there is no player target, keep patrolling.
        if (player == null)
        {
            CanSeePlayer = false;
            Patrol();
            return;
        }

        // If the player is dead, stop engaging and continue patrolling.
        if (playerHealth != null && playerHealth.CurrentHealth <= 0)
        {
            CanSeePlayer = false;
            Patrol();
            return;
        }

        // Check whether the enemy can currently see the player.
        CanSeePlayer = CheckVision();

        if (CanSeePlayer && player != null)
        {
            // Save the last known player position and face the player.
            LastKnownPlayerPosition = player.position;
            LookAtPlayer();

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Move closer only if the player is still outside the stopping distance.
            if (distanceToPlayer > stopDistance)
            {
                ChasePlayer();
            }

            // Fire when the cooldown has expired.
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown;
            }
        }
        else
        {
            // Resume patrol behavior when the player is not visible.
            Patrol();
        }

        // Log only when the player becomes visible for the first time in this detection cycle.
        if (CanSeePlayer && !wasSeeingPlayerLastFrame && debugLogDetection)
        {
            Debug.Log($"{name}: found player -> starts shooting!");
        }

        wasSeeingPlayerLastFrame = CanSeePlayer;
    }

    private bool CheckVision()
    {
        // Vision cannot work without a player or an eye point.
        if (player == null || eyePoint == null)
            return false;

        // Do not detect dead players.
        if (playerHealth != null && playerHealth.CurrentHealth <= 0)
            return false;

        // Aim roughly at the player's upper body.
        Vector3 target = player.position + Vector3.up * 1.0f;
        Vector3 toPlayer = target - eyePoint.position;

        float distance = toPlayer.magnitude;
        if (distance > viewDistance)
            return false;

        Vector3 direction = toPlayer.normalized;

        float angle = Vector3.Angle(eyePoint.forward, direction);
        if (angle > viewAngle * 0.5f)
            return false;

        // Use a sphere cast to simulate a more forgiving field of vision.
        if (Physics.SphereCast(
            eyePoint.position,
            sphereCastRadius,
            direction,
            out RaycastHit hit,
            distance,
            detectionMask,
            QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player || hit.transform.IsChildOf(player);
        }

        return false;
    }

    private void Patrol()
    {
        // Do nothing if no patrol points were assigned.
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];
        MoveTo(target.position, patrolSpeed);

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= stopDistance)
        {
            // Move on to the next waypoint and loop back to the start if needed.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void ChasePlayer()
    {
        // Safety check in case the player reference is missing.
        if (player == null)
            return;

        MoveTo(player.position, chaseSpeed);
    }

    private void MoveTo(Vector3 targetPosition, float moveSpeed)
    {
        // Keep movement on a flat plane by ignoring height differences.
        Vector3 flatTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        Vector3 direction = flatTarget - transform.position;

        if (direction.sqrMagnitude <= stopDistance * stopDistance)
            return;

        Vector3 moveDir = direction.normalized;

        // Move the enemy toward the target position.
        transform.position = Vector3.MoveTowards(
            transform.position,
            flatTarget,
            moveSpeed * Time.deltaTime
        );

        if (moveDir != Vector3.zero)
        {
            // Smoothly rotate toward the movement direction.
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void LookAtPlayer()
    {
        // Safety check in case the player reference is missing.
        if (player == null)
            return;

        // Rotate only horizontally toward the player.
        Vector3 lookTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
        Vector3 dir = (lookTarget - transform.position).normalized;

        if (dir == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void Shoot()
    {
        // Shooting requires a bullet prefab, a fire point, and a valid player target.
        if (bulletPrefab == null || firePoint == null || player == null)
            return;

        // Do not shoot at a dead player.
        if (playerHealth != null && playerHealth.CurrentHealth <= 0)
            return;

        // Aim at the player's upper body.
        Vector3 target = player.position + Vector3.up * 1.0f;
        Vector3 direction = (target - firePoint.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

        // Apply forward velocity if the projectile has a Rigidbody.
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * bulletSpeed;
        }

        // Play the shooting sound if both clip and audio source are available.
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Only draw debug gizmos when enabled and when an eye point exists.
        if (!debugDrawGizmos || eyePoint == null)
            return;

        // Draw the overall view range.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyePoint.position, viewDistance);

        // Draw the left and right boundaries of the field of view.
        Vector3 leftView = Quaternion.Euler(0, -viewAngle / 2f, 0) * eyePoint.forward;
        Vector3 rightView = Quaternion.Euler(0, viewAngle / 2f, 0) * eyePoint.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(eyePoint.position, leftView * viewDistance);
        Gizmos.DrawRay(eyePoint.position, rightView * viewDistance);

        // Draw the sphere cast radius at the eye point.
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(eyePoint.position, sphereCastRadius);

        if (firePoint != null)
        {
            // Draw the projectile spawn point.
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, 0.15f);
        }

        if (CanSeePlayer && player != null)
        {
            // Draw a red line to the player while the player is visible.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(eyePoint.position, player.position + Vector3.up * 1.0f);
        }
    }
}