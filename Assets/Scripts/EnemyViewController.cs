using UnityEngine;

public class EnemyViewController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform eyePoint;

    [Header("Movement")]
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float chaseSpeed = 4.5f;
    [SerializeField] private float stopDistance = 1.5f;
    [SerializeField] private float rotationSpeed = 6f;

    [Header("Patrol Points")]
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [Header("Vision")]
    [SerializeField] private float viewDistance = 15f;
    [SerializeField, Range(0f, 180f)] private float viewAngle = 90f;
    [SerializeField] private float sphereCastRadius = 0.35f;

    [Header("Layers")]
    [SerializeField] private LayerMask detectionMask;

    [Header("Debug")]
    [SerializeField] private bool debugLogDetection = true;
    [SerializeField] private bool debugDrawGizmos = true;

    public bool CanSeePlayer { get; private set; }
    public Vector3 LastKnownPlayerPosition { get; private set; }

    private bool wasSeeingPlayerLastFrame = false;

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    private void Update()
    {
        CanSeePlayer = CheckVision();

        if (CanSeePlayer && player != null)
        {
            LastKnownPlayerPosition = player.position;
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        if (CanSeePlayer && !wasSeeingPlayerLastFrame && debugLogDetection)
        {
            Debug.Log($"{name}: sees the player!");
        }

        wasSeeingPlayerLastFrame = CanSeePlayer;
    }

    private bool CheckVision()
    {
        if (player == null || eyePoint == null)
            return false;

        Vector3 target = player.position + Vector3.up * 1.0f;
        Vector3 toPlayer = target - eyePoint.position;

        float distance = toPlayer.magnitude;
        if (distance > viewDistance)
            return false;

        Vector3 direction = toPlayer.normalized;

        float angle = Vector3.Angle(eyePoint.forward, direction);
        if (angle > viewAngle * 0.5f)
            return false;

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
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];
        MoveTo(target.position, patrolSpeed);

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= stopDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    private void ChasePlayer()
    {
        if (player == null)
            return;

        MoveTo(player.position, chaseSpeed);
    }

    private void MoveTo(Vector3 targetPosition, float moveSpeed)
    {
        Vector3 flatTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        Vector3 direction = flatTarget - transform.position;

        if (direction.sqrMagnitude <= stopDistance * stopDistance)
            return;

        Vector3 moveDir = direction.normalized;

        transform.position = Vector3.MoveTowards(
            transform.position,
            flatTarget,
            moveSpeed * Time.deltaTime
        );

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!debugDrawGizmos || eyePoint == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(eyePoint.position, viewDistance);

        Vector3 leftView = Quaternion.Euler(0, -viewAngle / 2f, 0) * eyePoint.forward;
        Vector3 rightView = Quaternion.Euler(0, viewAngle / 2f, 0) * eyePoint.forward;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(eyePoint.position, leftView * viewDistance);
        Gizmos.DrawRay(eyePoint.position, rightView * viewDistance);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(eyePoint.position, sphereCastRadius);

        if (CanSeePlayer && player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(eyePoint.position, player.position + Vector3.up * 1.0f);
        }
    }
}