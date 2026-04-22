using UnityEngine;

// Controls monster enemy patrol movement between waypoints.
public class MonsterEnemy : MonoBehaviour
{
    [Header("Movement")]
    // Movement speed of the monster.
    public float speed = 3f;
    // Distance at which the monster stops before switching to the next waypoint.
    public float stopDistance = 1.5f;

    [Header("Prefabs")]
    // Monster prefab to instantiate.
    public GameObject monsterPrefab; // Monster prefab to instantiate

    [Header("Patrol Points")]
    // Assign patrol waypoints in the Inspector.
    public Transform[] waypoints;
    // Index of the current waypoint target.
    private int currentWaypointIndex = 0;

    void Update()
    {
        // Stop execution if the prefab or waypoint list is missing.
        if (monsterPrefab == null || waypoints == null || waypoints.Length == 0)
        {
            return;
        }

        // Get the current waypoint target.
        Transform target = waypoints[currentWaypointIndex];
        // Measure the distance to the current target waypoint.
        float dist = Vector3.Distance(transform.position, target.position);

        if (dist > stopDistance)
        {
            // Move the monster toward the current waypoint.
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // Switch to the next waypoint when the current one is reached.
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}