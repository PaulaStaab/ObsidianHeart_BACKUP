using UnityEngine;

public class WeaponProjectile : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 3f;

    [Header("Hit")]
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private int damage = 1;

    private float lifeTimer;

    private void Start()
    {
        lifeTimer = lifeTime;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (((1 << other.gameObject.layer) & hitMask.value) == 0)
    //        return;

    //    Health health = other.GetComponent<Health>();
    //    if (health != null)
    //    {
    //        health.TakeDamage(damage);
    //    }

    //    Destroy(gameObject);
    //}
}