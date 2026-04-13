using UnityEngine;

public class SpiderEnemy1 : MonoBehaviour
{
    [Header("Bewegung")]
    public float speed = 3f;

    [Header("Spawn")]
    public GameObject spiderPrefab;
    public int maxSpinnen = 3;

    private Transform pointA;
    private Transform pointB;
    private Transform currentTarget;

    private static int activeSpinnen = 0;

    void Start()
    {
        pointA = GameObject.Find("Spider_Spawnpoint")?.transform;
        pointB = GameObject.Find("SpiderPoint B")?.transform;

        if (pointA == null || pointB == null || spiderPrefab == null)
        {
            Debug.LogError("SpiderEnemy1: Spider_Spawnpoint, SpiderPoint B oder spiderPrefab fehlen!");
            Destroy(gameObject);
            return;
        }

        currentTarget = pointB;
        activeSpinnen++;
    }

    void Update()
    {
        Vector3 targetPos = new Vector3(
            currentTarget.position.x,
            transform.position.y,
            currentTarget.position.z
        );

        Vector3 direction = (targetPos - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.position += transform.forward * speed * Time.deltaTime;

        Vector3 flatCurrentPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 flatTargetPos = new Vector3(currentTarget.position.x, 0f, currentTarget.position.z);

        if (Vector3.Distance(flatCurrentPos, flatTargetPos) < 0.2f)
        {
            SpawnNextSpider();
            Destroy(gameObject);
        }
    }

    void SpawnNextSpider()
    {
        if (activeSpinnen < maxSpinnen)
        {
            GameObject newSpider = Instantiate(spiderPrefab, pointA.position, Quaternion.identity);
            SpiderEnemy1 spiderScript = newSpider.GetComponent<SpiderEnemy1>();

            if (spiderScript != null)
            {
                // keine weiteren Werte nötig, da Point A/B wieder automatisch gesucht werden
            }

            activeSpinnen++;
        }
    }

    void OnDestroy()
    {
        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
    }
}
