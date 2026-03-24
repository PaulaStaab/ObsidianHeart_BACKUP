using UnityEngine;
using TMPro;

public class RessourcenManager : MonoBehaviour
{
    public static RessourcenManager Instance { get; private set; }

    [Header("Ressourcen")]
    public int currentRessourcen = 0;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ressourcenText;   // Dein Zähler-Text

    [Header("Player-Erkennung")]
    [SerializeField] private string playerTag = "Player";

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("RessourcenManager gestartet!");
        UpdateUI();   // Initial anzeigen
    }

    public void AddRessourcen(int menge)
    {
        currentRessourcen += menge;
        Debug.Log($"Ressourcen hinzugefügt: {menge}, Gesamt: {currentRessourcen}");
        UpdateUI();
    }

    public bool SpendRessource(int kosten)
    {
        if (currentRessourcen >= kosten)
        {
            currentRessourcen -= kosten;
            Debug.Log($"Ressourcen gespendet: {kosten}, Rest: {currentRessourcen}");
            UpdateUI();
            return true;
        }
        Debug.Log($"Nicht genug! Brauche {kosten}, habe {currentRessourcen}");
        return false;
    }

    private void UpdateUI()
    {
        if (ressourcenText != null)
        {
            ressourcenText.text = "Ressourcen: " + currentRessourcen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag))
            return;

        Cratercollector collector = other.GetComponent<Cratercollector>();
        if (collector == null)
            collector = other.GetComponentInChildren<Cratercollector>();

        if (collector != null)
        {
            int gesammelteMenge = collector.NimmAlleRessourcen();
            if (gesammelteMenge > 0)
            {
                AddRessourcen(gesammelteMenge); // UI wird automatisch aktualisiert
            }
        }
    }
}
