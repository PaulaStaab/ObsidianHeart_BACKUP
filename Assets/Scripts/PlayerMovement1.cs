using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float runSpeed = 20f;

    [Header("Kamera Referenz")]
    [SerializeField] private Transform cameraTransform;     // das Feld erscheint im Inspector

    private Rigidbody rb;
    private bool isRunning = false;
    private Vector3 moveInput;


    void update()
    {
        float moveX = Input.GetAxis("Horizontal"); // Standard: A/D oder Pfeiltasten links/rechts
        float moveZ = Input.GetAxis("Vertical");   // Standard: W/S oder Pfeiltasten hoch/runter
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody fehlt am " + gameObject.name);
            enabled = false;
        }

        if (cameraTransform == null)
        {
            Debug.LogError("Keine Kamera im Inspector von " + gameObject.name + " zugewiesen!");
        }
    }

    // OnMove & OnSprint bleiben gleich
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
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