using UnityEngine;
using TMPro; // Für TextMeshProUGUI, falls verwendet

public class Cratercollector : MonoBehaviour
{
    [Header("UI Referenz")]
    [SerializeField] private TextMeshProUGUI ressourcenText; // Ziehe hier dein Text-Element aus dem Canvas rein

    private int ressourcenMenge = 0;
    private bool kannSammeln = true; // Verhindert mehrfaches Sammeln am selben Krater

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && kannSammeln)
        {
            kannSammeln = false;
            ressourcenMenge += 1; // +1 Ressource pro Krater
            UpdateUI();
            // Optional: Krater deaktivieren oder zerstören
            // gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        if (ressourcenText != null)
        {
            ressourcenText.text = "Ressourcen: " + ressourcenMenge;
        }
    }
}
