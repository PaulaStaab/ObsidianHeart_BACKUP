using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    public float speed = 20f;  // Höher für schnelles Fliegen wie im Screenshot
    public float lifetime = 10f;

    void Start()
    {
        // In transform.forward schießen (behält Turm-Richtung)
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }
}
