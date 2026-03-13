using UnityEngine;

public class SpiderEnemy1 : MonoBehaviour
{
    [Header("Bewegung")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    private float rotationVelocity = 0f;

    [Header("Spawn")]
    public GameObject spiderPrefab;
    public int maxSpinnen = 3;  // Max. gleichzeitige Spinnen (Anti-Spam)

    private Transform currentTarget;
    private static int activeSpinnen = 0;
    private float angle;

    void Start()
    {
        if (pointA == null || pointB == null || spiderPrefab == null)
        {
            Debug.LogError("SpiderEnemy1: PointA, PointB oder Prefab fehlen!");
            Destroy(gameObject);
            return;
        }

        currentTarget = pointB;  // Starte bei A, gehe zu B
        activeSpinnen++;
    }

    void Update()
    {
        // Horizontale Bewegung (kein Y-Fly)
        Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Stabile Y-Rotation (kein Spinnen!)
        Vector3 direction = (targetPos - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f; 
        transform.rotation = Quaternion.Euler(0, angle, 0);

        // Ziel erreicht? Despawn + nächste spawnen
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            SpawnNextSpider();
            Destroy(gameObject);  // Selbst zerstören
        }
    }

    void SpawnNextSpider()
    {
        if (activeSpinnen < maxSpinnen)
        {
            // Nächste bei PointA spawnen
            Instantiate(spiderPrefab, pointA.position, Quaternion.identity)
                .GetComponent<SpiderEnemy1>().currentTarget = pointB;
            activeSpinnen++;
        }
    }

    void OnDestroy()
    {
        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
    }
}
