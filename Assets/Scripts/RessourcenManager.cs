using UnityEngine;
using TMPro;

// Manages collected resources, updates the resource UI,
// and allows resource transfer from the player on trigger contact.
public class RessourcenManager : MonoBehaviour
{
    // Singleton instance so other scripts can access the resource manager globally.
    public static RessourcenManager Instance { get; private set; }

    [Header("Ressourcen")]
    // Current total amount of available resources.
    public int currentRessourcen = 0;

    [Header("UI")]
    // UI text used to display the current resource count.
    [SerializeField] private TextMeshProUGUI ressourcenText;

    [Header("Player-Erkennung")]
    // Tag used to detect the player in trigger events.
    [SerializeField] private string playerTag = "Player";

    protected virtual void Awake()
    {
        // Enforce the singleton pattern by destroying duplicate instances.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Store the global instance and keep it alive across scene loads.
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("RessourcenManager started!");
        UpdateUI(); // Show the initial value immediately.
    }

    public void AddRessourcen(int menge)
    {
        // Increase the current resource amount.
        currentRessourcen += menge;
        Debug.Log($"Resources added: {menge}, Total: {currentRessourcen}");
        UpdateUI();
    }

    public bool SpendRessource(int kosten)
    {
        // Spend resources only if enough are available.
        if (currentRessourcen >= kosten)
        {
            currentRessourcen -= kosten;
            Debug.Log($"Resources spent: {kosten}, Remaining: {currentRessourcen}");
            UpdateUI();
            return true;
        }
        Debug.Log($"Not enough! Need {kosten}, have {currentRessourcen}");
        return false;
    }

    private void UpdateUI()
    {
        // Refresh the UI text if a text element is assigned.
        if (ressourcenText != null)
        {
            ressourcenText.text = "Ressourcen: " + currentRessourcen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore objects that are not tagged as the player.
        if (!other.CompareTag(playerTag))
            return;

        // Try to find a Cratercollector component on the player or its children.
        Cratercollector collector = other.GetComponent<Cratercollector>();
        if (collector == null)
            collector = other.GetComponentInChildren<Cratercollector>();

        if (collector != null)
        {
            // Transfer all collected resources from the collector to the manager.
            int gesammelteMenge = collector.NimmAlleRessourcen();
            if (gesammelteMenge > 0)
            {
                AddRessourcen(gesammelteMenge); // UI is updated automatically.
            }
        }
    }
}