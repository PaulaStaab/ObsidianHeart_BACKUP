using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log("Rigidbody: " + (rb != null));  // Console check
    }

    public void OnMove(InputValue value)
    {
        Vector3 input = value.Get<Vector3>();
        Debug.Log("OnMove triggered: " + input);  // Muss erscheinen!
        if (rb) rb.linearVelocity = new Vector3(input.x, rb.linearVelocity.y, input.z) * speed;
    }
}
