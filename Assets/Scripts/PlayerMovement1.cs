using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float runSpeed = 20f;

    [Header("Kamera Referenz")]
    [SerializeField] private Transform cameraTransform;     // ← das Feld erscheint im Inspector

    private Rigidbody rb;
    private bool isRunning = false;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody fehlt am " + gameObject.name);
            enabled = false;
        }

        // Kamera wird jetzt per Inspector gesetzt → wir prüfen nur noch
        if (cameraTransform == null)
        {
            Debug.LogError("Keine Kamera im Inspector von " + gameObject.name + " zugewiesen!");
            // Optional: Versuch trotzdem MainCamera als Fallback
            if (Camera.main != null) cameraTransform = Camera.main.transform;
        }
    }

    // OnMove & OnSprint bleiben gleich
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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

        // Richtung relativ zur Kamera
        Vector3 moveDirection =
            cameraTransform.right * moveInput.x +
            cameraTransform.forward * moveInput.y;

        moveDirection = Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized;

        Vector3 velocity = moveDirection * currentSpeed;
        velocity.y = rb.linearVelocity.y;           // Gravitation / Y-Geschwindigkeit erhalten

        rb.linearVelocity = velocity;
    }
}