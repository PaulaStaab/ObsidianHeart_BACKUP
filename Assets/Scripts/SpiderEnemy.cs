using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    [Header("Bewegung")]
    public float speed = 2f;
    public Transform pointA;
    public Transform pointB;

    private Transform currentTarget;

    void Start()
    {
        if (pointA == null || pointB == null) return; // Optional: Prüfe Points
        currentTarget = pointA;
    }

    void Update()
    {
        // 3D-Version für deine Spinne
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
