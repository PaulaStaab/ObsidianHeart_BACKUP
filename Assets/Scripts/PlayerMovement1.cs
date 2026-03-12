using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 3f;
    public float runSpeed = 20f;
    private Rigidbody rb;
    private bool isRunning = false;
    private Transform cameraTransform;


    // Speicher für den aktuellen Input-Vektor
    private Vector3 moveInput;

    void Update()
    {
        // Bewegungseingaben abfragen
        float moveX = Input.GetAxis("Horizontal"); // Standard: A/D oder Pfeiltasten links/rechts
        float moveY = Input.GetAxis("Vertical");   // Standard: W/S oder Pfeiltasten hoch/runter

        // Richtung relativ zur Kamera berechnen
        Vector3 movementDirection = cameraTransform.right * moveX + cameraTransform.forward * moveY;
        movementDirection = Vector3.ProjectOnPlane(movementDirection, Vector3.up).normalized;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
        Debug.Log("Input empfangen: " + moveInput);
    }

    public void OnSprint(InputValue value)
    {
        isRunning = value.isPressed;
    }

    // FixedUpdate ist perfekt für Rigidbody-Bewegungen
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float currentSpeed = isRunning ? runSpeed : speed;

        Vector3 movement = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.z * currentSpeed);

        rb.linearVelocity = movement;
    }
}