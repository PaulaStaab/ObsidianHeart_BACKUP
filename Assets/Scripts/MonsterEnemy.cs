using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float stopDistance = 1.5f;

    [Header("Prefabs")]
    public GameObject monsterPrefab; // Monster-Prefab zum Instanziieren

    [Header("Patrol Points")]
    public Transform[] waypoints; // Weise Waypoints im Inspector zu
    private int currentWaypointIndex = 0;

    void Update()
    {
        if (monsterPrefab == null || waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];
        float dist = Vector2.Distance(transform.position, target.position);

        if (dist > stopDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            // Nächsten Waypoint wählen
            currentWaypointIndex = (currentWaypointIndex + 1 ) % waypoints.Length;
        }
    }
}
