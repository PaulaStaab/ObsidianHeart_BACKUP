using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

// Handles collecting resources when the player is close enough,
// updates the resource UI, and shows a temporary pickup popup.
public class Cratercollector : MonoBehaviour
{
    [Header("UI Referenzen")]
    // UI text that displays the current resource amount.
    [SerializeField] private TextMeshProUGUI ressourcenText;
    // Root object of the pickup popup UI.
    [SerializeField] private GameObject pickupPopupRoot;
    // Text element inside the pickup popup.
    [SerializeField] private TextMeshProUGUI pickupPopupText;

    [Header("Einstellungen")]
    // Optional old input setting left in the script as a comment.
    //[SerializeField] private KeyCode pickupKey = KeyCode.E;
    // Name of the collected resource shown in the popup.
    [SerializeField] private string ressourcenName = "Kristall";
    // Amount of resource added per pickup.
    [SerializeField] private int ressourcenWert = 1;
    // How long the pickup popup stays visible.
    [SerializeField] private float popupDauer = 1.0f;
    // Tag used to find the player object.
    [SerializeField] private string playerTag = "Player";

    [Header("Reichweite")]
    // Maximum distance at which the player can collect resources.
    [SerializeField] private float sammelReichweite = 5f;
    // Reserved player layer setting for range-related logic.
    [SerializeField] private LayerMask playerLayer = -1;

    // Locally tracked amount of collected resources.
    public int ressourcenMenge = 0;
    // True while the player is inside the collection range.
    private bool playerInReichweite = false;
    // Cached player transform used for distance checks.
    private Transform playerTransform;

    private void Update()
    {
        // Do nothing until a player reference has been found.
        if (playerTransform == null) return;

        // Check the current distance between this object and the player.
        float distanz = Vector3.Distance(transform.position, playerTransform.position);
        playerInReichweite = distanz <= sammelReichweite;

        // Allow pickup when the player is in range and presses the E key.
        if (playerInReichweite && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Aufsammeln();
        }
    }

    private void Aufsammeln()
    {
        // Add resources to the global resource manager if it exists.
        if (RessourcenManager.Instance != null)
        {
            RessourcenManager.Instance.AddRessourcen(ressourcenWert);
        }

        // Also increase the local resource counter stored in this script.
        ressourcenMenge += ressourcenWert;

        // Update the UI with the current global resource value.
        if (ressourcenText != null && RessourcenManager.Instance != null)
        {
            ressourcenText.text = "Ressourcen: " + RessourcenManager.Instance.currentRessourcen;
        }

        // Show the pickup popup after collecting.
        ZeigePopup();
    }

    private void UpdateUI()
    {
        // Updates the UI using the local resource counter.
        if (ressourcenText != null)
        {
            ressourcenText.text = "Ressourcen: " + ressourcenMenge;
        }
    }

    private void ZeigePopup()
    {
        // Stop if the popup UI references are missing.
        if (pickupPopupRoot == null || pickupPopupText == null) return;

        // Set the popup text and display the popup object.
        pickupPopupText.text = "+" + ressourcenWert + " " + ressourcenName;
        pickupPopupRoot.SetActive(true);
        // Restart the hide coroutine so the timer is refreshed on repeated pickups.
        StopAllCoroutines();
        StartCoroutine(PopupAusblenden());
    }

    private IEnumerator PopupAusblenden()
    {
        // Wait for the configured popup duration.
        yield return new WaitForSeconds(popupDauer);
        // Hide the popup if it still exists.
        if (pickupPopupRoot != null)
            pickupPopupRoot.SetActive(false);
    }

    private void Start()
    {
        // Find the player object by tag once at startup.
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    public int NimmAlleRessourcen()
    {
        // Return all locally stored resources and reset the local amount to zero.
        int menge = ressourcenMenge;
        ressourcenMenge = 0;
        UpdateUI();
        return menge;
    }
}