//////////using UnityEngine;

//////////public class SpiderEnemy1 : MonoBehaviour
//////////{
//////////    [Header("Bewegung")]
//////////    public Transform pointA;
//////////    public Transform pointB;
//////////    public float speed = 3f;

//////////    [Header("Spawn")]
//////////    public GameObject spiderPrefab;
//////////    public int maxSpinnen = 3;  // Max. gleichzeitige Spinnen (Anti-Spam)

//////////    private Transform currentTarget;
//////////    private static int activeSpinnen = 0;
//////////    private float angle;

//////////    void Start()
//////////    {
//////////        if (pointA == null || pointB == null || spiderPrefab == null)
//////////        {
//////////            Debug.LogError("SpiderEnemy1: PointA, PointB oder Prefab fehlen!");
//////////            Destroy(gameObject);
//////////            return;
//////////        }

//////////        currentTarget = pointB;  // Starte bei A, gehe zu B
//////////        activeSpinnen++;
//////////    }

//////////    void Update()
//////////    {
//////////        // Horizontale Bewegung (kein Y-Fly)
//////////        Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
//////////        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

//////////        // Stabile Y-Rotation (kein Spinnen!)
//////////        Vector3 direction = (targetPos - transform.position).normalized;
//////////        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f; 
//////////        transform.rotation = Quaternion.Euler(0, angle, 0);

//////////        // Ziel erreicht? Despawn + nächste spawnen
//////////        if (Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
//////////        {
//////////            SpawnNextSpider();
//////////            Destroy(gameObject);  // Selbst zerstören
//////////        }
//////////    }

//////////    void SpawnNextSpider()
//////////    {
//////////        if (activeSpinnen < maxSpinnen)
//////////        {
//////////            // Nächste bei PointA spawnen
//////////            Instantiate(spiderPrefab, pointA.position, Quaternion.identity)
//////////                .GetComponent<SpiderEnemy1>().currentTarget = pointB;
//////////            activeSpinnen++;
//////////        }
//////////    }

//////////    void OnDestroy()
//////////    {
//////////        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
//////////    }
//////////}


////////using UnityEngine;

////////public class SpiderEnemy1 : MonoBehaviour
////////{
////////    [Header("Bewegung")]
////////    public Transform pointA;
////////    public Transform pointB;
////////    public float speed = 3f;

////////    [Header("Spawn")]
////////    public GameObject spiderPrefab;
////////    public int maxSpinnen = 3;   // Max. gleichzeitige Spinnen

////////    private Transform currentTarget;
////////    private static int activeSpinnen = 0;

////////    void Start()
////////    {
////////        if (pointA == null || pointB == null || spiderPrefab == null)
////////        {
////////            Debug.LogError("SpiderEnemy1: PointA, PointB oder Prefab fehlen!");
////////            Destroy(gameObject);
////////            return;
////////        }

////////        currentTarget = pointB;
////////        activeSpinnen++;
////////    }

////////    void Update()
////////    {
////////        // Nur auf der X-Achse bewegen
////////        Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y, transform.position.z);
////////        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

////////        // Optional: Ausrichtung je nach Laufrichtung auf X
////////        Vector3 direction = targetPos - transform.position;
////////        if (direction.x > 0.01f)
////////        {
////////            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
////////        }
////////        else if (direction.x < -0.01f)
////////        {
////////            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
////////        }

////////        // Ziel erreicht?
////////        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < 0.2f)
////////        {
////////            SpawnNextSpider();
////////            Destroy(gameObject);
////////        }
////////    }

////////    void SpawnNextSpider()
////////    {
////////        if (activeSpinnen < maxSpinnen)
////////        {
////////            GameObject newSpider = Instantiate(spiderPrefab, pointA.position, Quaternion.identity);
////////            SpiderEnemy1 spiderScript = newSpider.GetComponent<SpiderEnemy1>();

////////            if (spiderScript != null)
////////            {
////////                spiderScript.pointA = pointA;
////////                spiderScript.pointB = pointB;
////////                spiderScript.currentTarget = pointB;
////////            }

////////            activeSpinnen++;
////////        }
////////    }

////////    void OnDestroy()
////////    {
////////        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
////////    }
////////}

//////using UnityEngine;

//////public class SpiderEnemy1 : MonoBehaviour
//////{
//////    [Header("Bewegung")]
//////    public Transform pointA;
//////    public Transform pointB;
//////    public float speed = 3f;

//////    [Header("Spawn")]
//////    public GameObject spiderPrefab;
//////    public int maxSpinnen = 3;  // Max. gleichzeitige Spinnen (Anti-Spam)

//////    private Transform currentTarget;
//////    private static int activeSpinnen = 0;
//////    private float angle;

//////    void Start()
//////    {
//////        if (pointA == null || pointB == null || spiderPrefab == null)
//////        {
//////            Debug.LogError("SpiderEnemy1: PointA, PointB oder Prefab fehlen!");
//////            Destroy(gameObject);
//////            return;
//////        }

//////        currentTarget = pointB;  // Starte bei A, gehe zu B
//////        activeSpinnen++;
//////    }

//////    void Update()
//////    {
//////        // Horizontale Bewegung (kein Y-Fly)
//////        Vector3 targetPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
//////        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

//////        // Stabile Y-Rotation (kein Spinnen!)
//////        Vector3 direction = (targetPos - transform.position).normalized;
//////        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + 180f;
//////        transform.rotation = Quaternion.Euler(0, angle, 0);

//////        // Ziel erreicht? Despawn + nächste spawnen
//////        if (Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
//////        {
//////            SpawnNextSpider();
//////            Destroy(gameObject);  // Selbst zerstören
//////        }
//////    }

//////    void SpawnNextSpider()
//////    {
//////        if (activeSpinnen < maxSpinnen)
//////        {
//////            // Nächste bei PointA spawnen
//////            Instantiate(spiderPrefab, pointA.position, Quaternion.identity)
//////                .GetComponent<SpiderEnemy1>().currentTarget = pointB;
//////            activeSpinnen++;
//////        }
//////    }

//////    void OnDestroy()
//////    {
//////        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
//////    }
//////}

////using UnityEngine;

////public class SpiderEnemy1 : MonoBehaviour
////{
////    [Header("Bewegung")]
////    public Transform pointA;
////    public Transform pointB;
////    public float speed = 3f;

////    [Header("Spawn")]
////    public GameObject spiderPrefab;
////    public int maxSpinnen = 3;

////    [Header("Ground Check")]
////    public LayerMask groundLayer;
////    public float groundCheckExtraHeight = 0.05f;

////    private Transform currentTarget;
////    private static int activeSpinnen = 0;
////    private BoxCollider boxCollider;
////    private bool isGrounded;

////    void Start()
////    {
////        if (pointA == null || pointB == null || spiderPrefab == null)
////        {
////            Debug.LogError("SpiderEnemy1: PointA, PointB oder Prefab fehlen!");
////            Destroy(gameObject);
////            return;
////        }

////        boxCollider = GetComponent<BoxCollider>();
////        if (boxCollider == null)
////        {
////            Debug.LogError("SpiderEnemy1: Kein BoxCollider im Parent gefunden!");
////            Destroy(gameObject);
////            return;
////        }

////        currentTarget = pointB;
////        activeSpinnen++;
////    }

////    void Update()
////    {
////        CheckGround();

////        if (!isGrounded)
////        {
////            return;
////        }

////        Vector3 targetPos = new Vector3(
////            currentTarget.position.x,
////            transform.position.y,
////            currentTarget.position.z
////        );

////        Vector3 direction = (targetPos - transform.position).normalized;

////        if (direction != Vector3.zero)
////        {
////            transform.rotation = Quaternion.LookRotation(direction);
////        }

////        transform.position += transform.forward * speed * Time.deltaTime;

////        if (Vector3.Distance(
////            new Vector3(transform.position.x, 0f, transform.position.z),
////            new Vector3(currentTarget.position.x, 0f, currentTarget.position.z)
////        ) < 0.2f)
////        {
////            SpawnNextSpider();
////            Destroy(gameObject);
////        }
////    }

////    void CheckGround()
////    {
////        Bounds bounds = boxCollider.bounds;

////        Vector3 checkCenter = new Vector3(
////            bounds.center.x,
////            bounds.min.y - groundCheckExtraHeight,
////            bounds.center.z
////        );

////        Vector3 checkHalfExtents = new Vector3(
////            bounds.extents.x * 0.95f,
////            0.05f,
////            bounds.extents.z * 0.95f
////        );

////        isGrounded = Physics.CheckBox(
////            checkCenter,
////            checkHalfExtents,
////            transform.rotation,
////            groundLayer
////        );
////    }

////    void SpawnNextSpider()
////    {
////        if (activeSpinnen < maxSpinnen)
////        {
////            GameObject newSpider = Instantiate(spiderPrefab, pointA.position, Quaternion.identity);
////            SpiderEnemy1 spiderScript = newSpider.GetComponent<SpiderEnemy1>();

////            if (spiderScript != null)
////            {
////                spiderScript.pointA = pointA;
////                spiderScript.pointB = pointB;
////                spiderScript.currentTarget = pointB;
////                spiderScript.groundLayer = groundLayer;
////            }

////            activeSpinnen++;
////        }
////    }

////    void OnDestroy()
////    {
////        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
////    }

////    void OnDrawGizmosSelected()
////    {
////        BoxCollider bc = GetComponent<BoxCollider>();
////        if (bc == null) return;

////        Bounds bounds = bc.bounds;

////        Vector3 checkCenter = new Vector3(
////            bounds.center.x,
////            bounds.min.y - groundCheckExtraHeight,
////            bounds.center.z
////        );

////        Vector3 checkHalfExtents = new Vector3(
////            bounds.extents.x * 0.95f,
////            0.05f,
////            bounds.extents.z * 0.95f
////        );

////        Gizmos.color = Color.green;
////        Gizmos.matrix = Matrix4x4.TRS(checkCenter, transform.rotation, Vector3.one);
////        Gizmos.DrawWireCube(Vector3.zero, checkHalfExtents * 2f);
////    }
////}

//using UnityEngine;

//public class SpiderEnemy1 : MonoBehaviour
//{
//    [Header("Bewegung")]
//    public float speed = 3f;

//    [Header("Spawn")]
//    public GameObject spiderPrefab;
//    public int maxSpinnen = 3;

//    [Header("Ground Check")]
//    public LayerMask groundLayer;
//    public float groundCheckExtraHeight = 0.05f;

//    private Transform pointA;
//    private Transform pointB;
//    private Transform currentTarget;

//    private static int activeSpinnen = 0;
//    private BoxCollider boxCollider;
//    private bool isGrounded;

//    void Start()
//    {
//        pointA = GameObject.Find("SpiderPoint A")?.transform;
//        pointB = GameObject.Find("SpiderPoint B")?.transform;

//        if (pointA == null || pointB == null || spiderPrefab == null)
//        {
//            Debug.LogError("SpiderEnemy1: SpiderPoint A, SpiderPoint B oder spiderPrefab fehlen!");
//            Destroy(gameObject);
//            return;
//        }

//        boxCollider = GetComponent<BoxCollider>();
//        if (boxCollider == null)
//        {
//            Debug.LogError("SpiderEnemy1: Kein BoxCollider im Parent gefunden!");
//            Destroy(gameObject);
//            return;
//        }

//        currentTarget = pointB;
//        activeSpinnen++;
//    }

//    void Update()
//    {
//        CheckGround();

//        if (!isGrounded)
//            return;

//        Vector3 targetPos = new Vector3(
//            currentTarget.position.x,
//            transform.position.y,
//            currentTarget.position.z
//        );

//        Vector3 direction = (targetPos - transform.position).normalized;

//        if (direction != Vector3.zero)
//        {
//            transform.rotation = Quaternion.LookRotation(direction);
//        }

//        transform.position += transform.forward * speed * Time.deltaTime;

//        Vector3 flatCurrentPos = new Vector3(transform.position.x, 0f, transform.position.z);
//        Vector3 flatTargetPos = new Vector3(currentTarget.position.x, 0f, currentTarget.position.z);

//        if (Vector3.Distance(flatCurrentPos, flatTargetPos) < 0.2f)
//        {
//            SpawnNextSpider();
//            Destroy(gameObject);
//        }
//    }

//    void CheckGround()
//    {
//        Bounds bounds = boxCollider.bounds;

//        Vector3 checkCenter = new Vector3(
//            bounds.center.x,
//            bounds.min.y - groundCheckExtraHeight,
//            bounds.center.z
//        );

//        Vector3 checkHalfExtents = new Vector3(
//            bounds.extents.x * 0.95f,
//            0.05f,
//            bounds.extents.z * 0.95f
//        );

//        isGrounded = Physics.CheckBox(
//            checkCenter,
//            checkHalfExtents,
//            transform.rotation,
//            groundLayer
//        );
//    }

//    void SpawnNextSpider()
//    {
//        if (activeSpinnen < maxSpinnen)
//        {
//            GameObject newSpider = Instantiate(spiderPrefab, pointA.position, Quaternion.identity);
//            SpiderEnemy1 spiderScript = newSpider.GetComponent<SpiderEnemy1>();

//            if (spiderScript != null)
//            {
//                spiderScript.groundLayer = groundLayer;
//            }

//            activeSpinnen++;
//        }
//    }

//    void OnDestroy()
//    {
//        activeSpinnen = Mathf.Max(0, activeSpinnen - 1);
//    }

//    void OnDrawGizmosSelected()
//    {
//        BoxCollider bc = GetComponent<BoxCollider>();
//        if (bc == null) return;

//        Bounds bounds = bc.bounds;

//        Vector3 checkCenter = new Vector3(
//            bounds.center.x,
//            bounds.min.y - groundCheckExtraHeight,
//            bounds.center.z
//        );

//        Vector3 checkHalfExtents = new Vector3(
//            bounds.extents.x * 0.95f,
//            0.05f,
//            bounds.extents.z * 0.95f
//        );

//        Gizmos.color = Color.green;
//        Gizmos.matrix = Matrix4x4.TRS(checkCenter, transform.rotation, Vector3.one);
//        Gizmos.DrawWireCube(Vector3.zero, checkHalfExtents * 2f);
//    }
//}

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
        pointA = GameObject.Find("SpiderPoint A")?.transform;
        pointB = GameObject.Find("SpiderPoint B")?.transform;

        if (pointA == null || pointB == null || spiderPrefab == null)
        {
            Debug.LogError("SpiderEnemy1: SpiderPoint A, SpiderPoint B oder spiderPrefab fehlen!");
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