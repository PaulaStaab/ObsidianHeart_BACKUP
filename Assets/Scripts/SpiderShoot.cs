using UnityEngine;

public class SpiderShoot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;

    [Header("Timing")]
    [SerializeField] private float shootInterval = 2f;

    [Header("Debug")]
    [SerializeField] private bool canShoot = true;

    private float timer = 0f;

    private void Update()
    {
        if (!canShoot) return;
        if (bulletPrefab == null || shootPoint == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}