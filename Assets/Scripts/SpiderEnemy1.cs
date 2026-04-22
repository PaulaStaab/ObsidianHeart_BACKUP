using UnityEngine;

// Moves a spider enemy from point A to point B and optionally spawns a replacement spider
// when the target point is reached, while tracking the number of active spiders.
public class SpiderEnemy1 : MonoBehaviour
{
    [Header("Bewegung")]
    // Movement speed of the spider.
    public float speed = 3f;

    [Header("Spawn")]
    // Prefab used to spawn the next spider.
    public GameObject spiderPrefab;
    // Maximum number of active spiders allowed at the same time.
    public int maxSpinnen = 3;

    // Spawn/start point of the spider.
    private Transform pointA;
    // Target point the spider moves toward.
    private Transform pointB;
    // Current movement target.
    private Transform currentTarget;

    // Shared counter for all active spider instances.
    private static int activeSpinnen = 0;

    void Start()
    {
        // Find the required scene points by name.
        pointA = GameObject.Find("Spider_Spawnpoint")?.transform;
        pointB = GameObject.Find("SpiderPoint B")?.transform;

        // Stop and destroy this object if required references are missing.
        if (pointA == null || pointB == null || spiderPrefab == null)
        {
            Debug.LogError("SpiderEnemy1: Spider_Spawnpoint, SpiderPoint B or spiderPrefab are missing!");
            Destroy(gameObject);
            return;
        }

        // Set the initial target and count this spider as active.
        currentTarget = pointB;
        activeSpinnen++;
    }

    void Update()
    {
        // Keep the target position on the spider's current height level.
        Vector3 targetPos = new Vector3(
            currentTarget.position.x,
            transform.position.y,
            currentTarget.position.z
        );

        // Calculate the normalized movement direction toward the target.
        Vector3 direction = (targetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            // Rotate the spider to face the movement direction.
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Move the spider forward.
        transform.position += transform.forward * speed * Time.deltaTime;

        // Compare positions on a flat plane so height differences do not matter.
        Vector3 flatCurrentPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatTargetPos = new Vector3(currentTarget.position.x, 0f, currentTarget.position.z);

        // When the spider reaches the target, spawn the next one and destroy this instance.
        if (Vector3.Distance(flatCurrentPos, flatTargetPos) < 0.2f)
        {
            SpawnNextSpider();
            Destroy(gameObject);
        }
    }

    void SpawnNextSpider()
    {
        // Spawn a new spider only if the active spider count is below the maximum.
        if (activeSpinnen < maxSpinnen)
        {
            GameObject newSpider = Instantiate(spiderPrefab, pointA.position, Quaternion.identity);
            SpiderEnemy1 spiderScript = newSpider.GetComponent<SpiderEnemy1>();

            if (spiderScript != null)
            {
                // Reserved block in case spawned spider setup is needed later.
            }

            activeSpinnen++;
        }
    }

    void OnDestroy()
    {
        // Decrease the shared active spider counter, but never below zero.
        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
    }
}