using System.Collections;
using UnityEngine;

public class TowerRotation : MonoBehaviour
{
    public float rotationSpeed = 45f; // Grad pro Sekunde
    public float pauseDuration = 1f;  // Pausenzeit in Sekunden

    void Start()
    {
        StartCoroutine(RotateBackAndForth());
    }

    IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            // Zum Halbkreis (180 Grad) rotieren
            Quaternion startRot = transform.rotation;
            Quaternion endRot = startRot * Quaternion.Euler(0, 180f, 0);
            float elapsed = 0f;
            while (elapsed < 1f)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed);
                elapsed += Time.deltaTime * rotationSpeed / 180f;
                yield return null;
            }
            transform.rotation = endRot; // Exakte Endrotation sicherstellen [web:19]

            // Pause
            yield return new WaitForSeconds(pauseDuration);

            // Zurück rotieren
            startRot = transform.rotation;
            endRot = Quaternion.Euler(0, 0, 0); // Oder ursprüngliche Rotation
            elapsed = 0f;
            while (elapsed < 1f)
            {
                transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed);
                elapsed += Time.deltaTime * rotationSpeed / 180f;
                yield return null;
            }
            transform.rotation = endRot;

            // Pause am Start
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}