using UnityEngine;

public class TowerLeveling_Tower2 : MonoBehaviour
{
    public int level = 1;

    // Prefabs für die 3 Stufen
    public GameObject towerLevel2Prefab;
    public GameObject towerLevel3Prefab;

    // Referenz auf das aktuell sichtbare Tower-Objekt
    private GameObject aktuellerTower;

    // Kosten pro Level (einfaches Beispiel)
    public int basisKosten = 20;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H gedrückt bei Tower " + name);
            VersucheUpgrade();
        }
    }

    public void VersucheUpgrade()
    {
        int kosten = basisKosten * level;  // Level 0 = 0 Kosten (platzieren)
        Debug.Log($"Upgrade-Versuch: Level={level}, Kosten={kosten}");

        if (RessourcenManager.Instance.SpendRessource(kosten))
        {
            level++;
            Debug.Log("ERFOLG! Neues Level: " + level);
            SetzeTowerAufLevel(level);
        }
        else
        {
            Debug.Log("Nicht genug Ressourcen!");
        }
    }
    private void SetzeTowerAufLevel(int neuesLevel)
    {
        // alten visuellen Tower löschen
        if (aktuellerTower != null)
        {
            Destroy(aktuellerTower);
        }

        GameObject prefab = null;

        switch (neuesLevel)
        {
            case 1:
                prefab = towerLevel2Prefab;
                break;
            case 2:
                prefab = towerLevel3Prefab;
                break;
            default:
                // Max-Level erreicht, nichts mehr ändern
                prefab = towerLevel3Prefab;
                level = 3;
                break;
        }

        // neuen Tower am gleichen Platz erzeugen
        aktuellerTower = Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform // als Kind dieses GameObjects
        );
    }
}
