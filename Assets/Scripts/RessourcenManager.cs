using UnityEngine;

public class RessourcenManager : MonoBehaviour
{
    public static RessourcenManager Instance { get; private set; }  // Property!
    public int currentRessourcen = 0;

    // *** WICHTIG: protected virtual *** 
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
    }

    public bool SpendRessource(int kosten)
    {
        if (currentRessourcen >= kosten)
        {
            currentRessourcen -= kosten;
            Debug.Log($"Ressourcen gespendet: {kosten}, Rest: {currentRessourcen}");
            return true;
        }
        Debug.Log($"Nicht genug! Brauche {kosten}, habe {currentRessourcen}");
        return false;
    }
}
