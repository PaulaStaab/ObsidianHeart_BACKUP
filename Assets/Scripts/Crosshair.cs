using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject crosshairUI;

    [Header("Detection")]
    [SerializeField] private float detectionRange = 100f;
    [SerializeField] private LayerMask hitMask;

    private void Update()
    {
        if (playerCamera == null || crosshairUI == null)
            return;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        bool showCrosshair = false;

        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, hitMask))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                showCrosshair = true;
            }
        }

        crosshairUI.SetActive(showCrosshair);
    }
}