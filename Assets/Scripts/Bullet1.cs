using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public float speed = 50f;
    public float lifetime = 500f;
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Damage an Enemy
        MonsterHealth monster = collision.gameObject.GetComponent<MonsterHealth>();
        SpiderHealth spider = collision.gameObject.GetComponent<SpiderHealth>();

        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
        else if (spider != null)
        {
            spider.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
