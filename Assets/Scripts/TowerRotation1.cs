using System.Collections;
using UnityEngine;

// This script rotates an object back and forth between two angles
// with a pause in between each rotation.
public class TowerRotation1 : MonoBehaviour
{
    public float rotationSpeed = 45f;   // Rotation speed in degrees per second
    public float pauseDuration = 1f;    // Pause time at each end position
    public float rotationAngle = 90f;   // Angle to rotate from the starting position

    private Quaternion initialRotation; // Starting rotation of the object
    private Quaternion targetRotation;  // Target rotation (initial + rotationAngle)

    void Start()
    {
        // Store the initial local rotation of the object
        initialRotation = transform.localRotation;

        // Calculate the target rotation by rotating around the Y-axis
        targetRotation = initialRotation * Quaternion.Euler(0, rotationAngle, 0);

        // Start the rotation loop coroutine
        StartCoroutine(RotateBackAndForth());
    }

    // Coroutine that continuously rotates between two positions
    IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            // Rotate from initial to target rotation
            yield return RotateTo(initialRotation, targetRotation);

            // Wait for a short pause
            yield return new WaitForSeconds(pauseDuration);

            // Rotate back from target to initial rotation
            yield return RotateTo(targetRotation, initialRotation);

            // Wait again before repeating
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    // Coroutine that smoothly rotates from one rotation to another
    IEnumerator RotateTo(Quaternion from, Quaternion to)
    {
        // Calculate the angle between the two rotations
        float angle = Quaternion.Angle(from, to);

        // Determine how long the rotation should take based on speed
        float duration = angle / rotationSpeed;

        float elapsed = 0f;

        // Interpolate rotation over time
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // Smoothly interpolate between the two rotations
            transform.localRotation = Quaternion.Slerp(from, to, t);

            // Increase elapsed time
            elapsed += Time.deltaTime;

            yield return null; // Wait until next frame
        }

        // Ensure the final rotation is set exactly
        transform.localRotation = to;
    }
}