using UnityEngine;

public class Bullet3D : MonoBehaviour
{
    public float speed = 30f;
    public float lifetime = 5f;
    public int damage = 1;

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
