using UnityEngine;
using UnityEngine.InputSystem;

// Handles player movement relative to the camera,
// including normal movement and sprinting.
public class PlayerMovement1 : MonoBehaviour
{
    // Base walking speed of the player.
    [SerializeField] private float speed = 3f;
    // Sprint speed used while the sprint input is active.
    [SerializeField] private float runSpeed = 20f;
    // Camera transform used to move relative to the camera direction.
    [SerializeField] private Transform cameraTransform;

    // Cached Rigidbody component for physics-based movement.
    private Rigidbody rb;
    // True while the sprint input is being held.
    private bool isRunning = false;
    // Current movement input read from the input system.
    private Vector3 moveInput;

    void Awake()
    {
        // Cache the Rigidbody component on this object.
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing");
            enabled = false;
            return;
        }

        // Warn if no camera transform was assigned.
        if (cameraTransform == null)
        {
            Debug.LogError("Camera missing");
        }
    }

    public void OnMove(InputValue value)
    {
        // Read the movement input as a Vector3.
        moveInput = value.Get<Vector3>();
        Debug.Log("Move Input Vector3: " + moveInput);
    }

    public void OnSprint(InputValue value)
    {
        // Enable or disable sprinting based on the input state.
        isRunning = value.isPressed;
    }

    void FixedUpdate()
    {
        // Apply movement during the physics update.
        MovePlayer();
    }

    void MovePlayer()
    {
        // Stop if required references are missing.
        if (rb == null || cameraTransform == null) return;

        // Choose the correct speed depending on sprint state.
        float currentSpeed = isRunning ? runSpeed : speed;

        // Build movement relative to the camera orientation.
        Vector3 moveDirection =
            cameraTransform.right * moveInput.x +
            cameraTransform.forward * moveInput.z +
            transform.up * moveInput.y;

        // Keep movement on a flat plane and normalize the direction.
        moveDirection = Vector3.ProjectOnPlane(moveDirection, Vector3.up).normalized;

        // Create the new velocity while preserving vertical Rigidbody movement.
        Vector3 velocity = moveDirection * currentSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
    }
}