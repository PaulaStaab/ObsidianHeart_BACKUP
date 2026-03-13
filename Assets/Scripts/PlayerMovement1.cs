using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float runSpeed = 20f;
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private bool isRunning = false;
    private Vector3 moveInput;  // Jetzt Vector3

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) { Debug.LogError("Rigidbody fehlt"); enabled = false; return; }
        if (cameraTransform == null) { Debug.LogError("Kamera fehlt"); }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();  // Vector3 lesen
        Debug.Log("Move Input Vector3: " + moveInput);  // Testen
    }

    public void OnSprint(InputValue value)
    {
        isRunning = value.isPressed;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (rb == null || cameraTransform == null) return;

        float currentSpeed = isRunning ? runSpeed : speed;

        // moveInput.x/y/z direkt nutzen (z.B. x=links/rechts, y=vorne/rück? Anpassen!)
        Vector3 moveDirection =
            cameraTransform.right * moveInput.x +
            cameraTransform.forward * moveInput.z +  // z für vor/zurück bei Vector3
            transform.up * moveInput.y;  // y für hoch/runter, falls gewollt

        moveDirection = Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized;
        Vector3 velocity = moveDirection * currentSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }
}
