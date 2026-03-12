using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    [Header("Bewegung")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    [Header("Prefab")]
    public GameObject spiderPrefab; // Hier dein Spinnen-Prefab reinziehen

    private Transform currentTarget;

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        if (spiderPrefab == null)
            return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {currentTarget = currentTarget == pointA ? pointB : pointA;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale; 
        }
    }
}
