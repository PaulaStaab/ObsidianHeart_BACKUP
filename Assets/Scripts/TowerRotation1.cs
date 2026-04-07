using System.Collections;
using UnityEngine;

public class TowerRotation1 : MonoBehaviour
{
    public float rotationSpeed = 45f; // Speed per second
    public float pauseDuration = 1f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    void Start()
    {
        initialRotation = transform.localRotation;
        targetRotation = initialRotation * Quaternion.Euler(0, 90f, 0);

        StartCoroutine(RotateBackAndForth());
    }

    IEnumerator RotateBackAndForth()
    {
        while (true)
        {
            yield return RotateTo(initialRotation, targetRotation);
            yield return new WaitForSeconds(pauseDuration);

            yield return RotateTo(targetRotation, initialRotation);
            yield return new WaitForSeconds(pauseDuration);
        }
    }

    IEnumerator RotateTo(Quaternion from, Quaternion to)
    {
        float angle = Quaternion.Angle(from, to);
        float duration = angle / rotationSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localRotation = Quaternion.Slerp(from, to, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = to;
    }
}