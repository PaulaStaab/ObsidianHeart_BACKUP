////using UnityEngine;

////public class TowerLeveling_Tower1_2 : MonoBehaviour
////{
////    public int level = 1;

////    // Prefabs für die 3 Stufen
////    public GameObject towerLevel2Prefab;
////    public GameObject towerLevel3Prefab;

////    // Referenz auf das aktuell sichtbare Tower-Objekt
////    private GameObject aktuellerTower;

////    // Kosten pro Level (einfaches Beispiel)
////    public int basisKosten = 20;
////    void Update()
////    {
////        if (Input.GetKeyDown(KeyCode.H))
////        {
////            Debug.Log("H gedrückt bei Tower " + name);
////            VersucheUpgrade();
////        }
////    }

////    public void VersucheUpgrade()
////    {
////        if (RessourcenManager.Instance == null)
////        {
////            Debug.LogError("RessourcenManager.Instance ist NULL! Prüfe Singleton!");
////            return;
////        }

////        int kosten = basisKosten * level;
////        Debug.Log($"Upgrade: Level={level}, Kosten={kosten}");

////        if (RessourcenManager.Instance.SpendRessource(kosten))
////        {
////            level++;
////            SetzeTowerAufLevel(level);
////        }
////        else
////        {
////            Debug.Log("Nicht genug Ressourcen!");
////        }
////    }



////    private void SetzeTowerAufLevel(int neuesLevel)
////    {
////        // alten visuellen Tower löschen
////        if (aktuellerTower != null)
////        {
////            Destroy(aktuellerTower);
////        }

////        GameObject prefab = null;

////        switch (neuesLevel)
////        {
////            case 1:
////                prefab = towerLevel2Prefab;
////                break;
////            case 2:
////                prefab = towerLevel3Prefab;
////                break;
////            default:
////                // Max-Level erreicht, nichts mehr ändern
////                prefab = towerLevel3Prefab;
////                level = 3;
////                break;
////        }

////        // neuen Tower am gleichen Platz erzeugen
////        aktuellerTower = Instantiate(
////            prefab,
////            transform.position,
////            transform.rotation,
////            transform // als Kind dieses GameObjects
////        );
////    }
////}

//using UnityEngine;

//public class TowerLeveling_Tower1_2 : MonoBehaviour
//{
//    [Header("Level-System")]
//    public int level = 1;
//    public int basisKosten = 20;

//    [Header("Kanonen (als Childs)")]
//    public GameObject kanone1;
//    public GameObject kanone2;
//    public GameObject kanone3;

//    [Header("Visuelle Level-Upgrades (optional)")]
//    public GameObject level2Visual;
//    public GameObject level3Visual;

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.H))
//        {
//            Debug.Log("H gedrückt bei Tower " + name);
//            VersucheUpgrade();
//        }
//    }

//    public void VersucheUpgrade()
//    {
//        if (RessourcenManager.Instance == null)
//        {
//            Debug.LogError("RessourcenManager.Instance ist NULL!");
//            return;
//        }

//        int kosten = basisKosten * level;
//        Debug.Log($"Upgrade: Level={level}, Kosten={kosten}");

//        if (RessourcenManager.Instance.SpendRessource(kosten))
//        {
//            level = Mathf.Min(level + 1, 3); // Max Level 3
//            SetzeTowerAufLevel(level);
//            Debug.Log($"Tower erfolgreich auf Level {level} upgegradet!");
//        }
//        else
//        {
//            Debug.Log("Nicht genug Ressourcen!");
//        }
//    }

//    private void SetzeTowerAufLevel(int neuesLevel)
//    {
//        // Kanone 1 immer aktiv
//        if (kanone1 != null) kanone1.SetActive(true);

//        // Level-spezifische Kanonen
//        if (kanone2 != null) kanone2.SetActive(neuesLevel >= 2);
//        if (kanone3 != null) kanone3.SetActive(neuesLevel >= 3);

//        // Visuelle Upgrades (optional)
//        if (level2Visual != null) level2Visual.SetActive(neuesLevel >= 2);
//        if (level3Visual != null) level3Visual.SetActive(neuesLevel >= 3);
//    }

//    // Aufrufbar aus UI-Button
//    public void UpgradeButton()
//    {
//        VersucheUpgrade();
//    }
//}

using UnityEngine;

public class TowerLeveling_Tower2 : MonoBehaviour
{
    [Header("Level-System")]
    public int level = 1;
    public int basisKosten = 20;

    [Header("Kanonen (als Childs dieses Turms)")]
    public GameObject kanone1;
    public GameObject kanone2;
    public GameObject kanone3;

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
        if (RessourcenManager.Instance == null)
        {
            Debug.LogError("RessourcenManager.Instance ist NULL!");
            return;
        }

        int kosten = basisKosten * level;
        Debug.Log($"Upgrade: Level={level}, Kosten={kosten}");

        if (RessourcenManager.Instance.SpendRessource(kosten))
        {
            level = Mathf.Min(level + 1, 3); // Max Level 3
            SetzeTowerAufLevel(level);
            Debug.Log($"Tower erfolgreich auf Level {level} upgegradet!");
        }
        else
        {
            Debug.Log("Nicht genug Ressourcen!");
        }
    }

    private void SetzeTowerAufLevel(int neuesLevel)
    {
        // Kanone 1 immer aktiv (Level 1+)
        if (kanone1 != null) kanone1.SetActive(true);

        // Level 2: Zweite Kanone aktivieren
        if (kanone2 != null) kanone2.SetActive(neuesLevel >= 2);

        // Level 3: Dritte Kanone aktivieren  
        if (kanone3 != null) kanone3.SetActive(neuesLevel >= 3);
    }

    // Aufrufbar aus UI-Button (z.B. Upgrade-Button)
    public void UpgradeButton()
    {
        VersucheUpgrade();
    }
}