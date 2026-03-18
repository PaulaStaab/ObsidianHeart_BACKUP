using UnityEngine;

public class RessourcenManager : MonoBehaviour
{
    public static RessourcenManager Instance;

    public int metal; // deine Währung, z.B. "Kristalle"

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Reset auf 0 für neuen Spielrun (kein Laden aus PlayerPrefs)
        metal = 0;
    }

    public void RessourcenHinzufügen(int menge)
    {
        metal += menge;
        Debug.Log("Ressourcen: " + metal);
    }

    public bool RessourcenAusgeben(int menge)
    {
        if (metal < menge) return false;
        metal -= menge;
        Debug.Log("Ressourcen nach Ausgabe: " + metal);
        return true;
    }
}
