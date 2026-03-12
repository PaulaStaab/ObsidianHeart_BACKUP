using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Einstellungen")]
    public float mouseSensitivityX = 250f;  // Sensitivität horizontal (links/rechts)
    public float mouseSensitivityY = 250f;  // Sensitivität vertikal (hoch/runter)

    public float minimumY = -60f;           // Max runter schauen
    public float maximumY = 60f;            // Max hoch schauen

    private float rotationY = 0f;           // Aktuelle vertikale Rotation (X-Achse)

    void Update()
    {
        // Mausachsen abfragen
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // X-Rotation (hoch/runter) clampen
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        // Y-Rotation (links/rechts) einfach addieren
        transform.localRotation = Quaternion.Euler(rotationY, transform.localEulerAngles.y + mouseX, 0);
    }

    void Start()
    {
        // Cursor im Playmode sperren
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
