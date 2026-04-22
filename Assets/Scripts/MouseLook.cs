using UnityEngine;

// Handles first-person style mouse look rotation and clamps vertical camera movement.
public class MouseLook : MonoBehaviour
{
    [Header("Einstellungen")]
    // Horizontal mouse sensitivity.
    public float mouseSensitivityX = 250f;
    // Vertical mouse sensitivity.
    public float mouseSensitivityY = 250f;

    // Minimum vertical look angle.
    public float minimumY = -60f;
    // Maximum vertical look angle.
    public float maximumY = 60f;
    // Stores the current vertical rotation value.
    private float rotationY = 0f;

    void Update()
    {
        // Read mouse movement input and scale it by sensitivity and frame time.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Update and clamp the vertical rotation.
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        // Apply the combined vertical and horizontal local rotation.
        transform.localRotation = Quaternion.Euler(rotationY, transform.localEulerAngles.y + mouseX, 0);
    }

    void Start()
    {
        // Lock and hide the cursor when the scene starts.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}