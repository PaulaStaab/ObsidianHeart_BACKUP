using UnityEngine;

public class MonsterEnemy : MonoBehaviour
{
    [Header("Bewegung")]
    public float speed = 3f;
    public float stopDistance = 1.5f;

    [Header("Prefabs")]
    public GameObject monsterPrefab; // Monster-Prefab


    void Update()
    {
        if (monsterPrefab == null || playerTarget == null)
            return;

        float dist = Vector2.Distance(transform.position, playerTarget.position);

        if (dist > stopDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                playerTarget.position,
                speed * Time.deltaTime
            );
        }
    }
}
