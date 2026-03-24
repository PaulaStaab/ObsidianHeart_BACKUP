using UnityEngine;
using TMPro;
using System.Collections;

public class Cratercollector : MonoBehaviour
{
    [Header("UI Referenzen")]
    [SerializeField] private TextMeshProUGUI ressourcenText;        // Oben rechts Zähler
    [SerializeField] private GameObject pickupPopupRoot;            // Popup Panel Mitte
    [SerializeField] private TextMeshProUGUI pickupPopupText;       // Popup Text Mitte

    [Header("Einstellungen")]
    [SerializeField] private KeyCode pickupKey = KeyCode.E;
    [SerializeField] private string ressourcenName = "Kristall";
    [SerializeField] private int ressourcenWert = 1;
    [SerializeField] private float popupDauer = 1.0f;
    [SerializeField] private string playerTag = "Player";

    [Header("Reichweite")]
    [SerializeField] private float sammelReichweite = 5f;          // Abstand zum Krater
    [SerializeField] private LayerMask playerLayer = -1;           // Player Layer

    public int ressourcenMenge = 0;
    private bool playerInReichweite = false;
    private Transform playerTransform;

    private void Update()
    {
        if (playerTransform == null) return;

        // Distanz prüfen (VOR dem Krater)
        float distanz = Vector3.Distance(transform.position, playerTransform.position);
        playerInReichweite = distanz <= sammelReichweite;

        if (playerInReichweite && Input.GetKeyDown(pickupKey))
        {
            Aufsammeln();
        }
    }

    private void Aufsammeln()
    {
        // 1) Ressource im RessourcenManager erhöhen
        if (RessourcenManager.Instance != null)
        {
            RessourcenManager.Instance.AddRessourcen(ressourcenWert);
        }

        // 2) Optional: Lokaler Zähler nur für Anzeige (falls du ihn noch brauchst)
        ressourcenMenge += ressourcenWert;

        // 3) UI-Text aktualisieren (globaler Wert)
        if (ressourcenText != null && RessourcenManager.Instance != null)
        {
            ressourcenText.text = "Ressourcen: " + RessourcenManager.Instance.currentRessourcen;
        }

        ZeigePopup();
    }


    private void UpdateUI()
    {
        if (ressourcenText != null)
        {
            ressourcenText.text = "Ressourcen: " + ressourcenMenge;
        }
    }

    private void ZeigePopup()
    {
        if (pickupPopupRoot == null || pickupPopupText == null) return;

        pickupPopupText.text = "+" + ressourcenWert + " " + ressourcenName;
        pickupPopupRoot.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(PopupAusblenden());
    }

    private IEnumerator PopupAusblenden()
    {
        yield return new WaitForSeconds(popupDauer);
        if (pickupPopupRoot != null)
            pickupPopupRoot.SetActive(false);
    }

    // Automatisch Player finden
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
            playerTransform = playerObj.transform;
    }

    // Wird vom RessourcenManager aufgerufen, wenn der Player in seinen Trigger läuft
    public int NimmAlleRessourcen()
    {
        int menge = ressourcenMenge;
        ressourcenMenge = 0;
        UpdateUI();
        return menge;
    }
}

