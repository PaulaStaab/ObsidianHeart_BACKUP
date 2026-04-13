using UnityEngine;
using UnityEngine.InputSystem;

public class TowerLeveling_Tower1 : MonoBehaviour
{
    public int level = 1;

    public GameObject towerLevel2Prefab;
    public GameObject towerLevel3Prefab;

    private GameObject aktuellerTower;

    public int basisKosten = 20;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame)
        {
            Debug.Log("H gedrückt bei Tower " + name);
            VersucheUpgrade();
        }
    }

    public void VersucheUpgrade()
    {
        if (level == 0)
        {
            // Platzieren
        }
        else
        {
            Debug.Log("Nicht genug Ressourcen für Upgrade!");
        }
    }

    private void SetzeTowerAufLevel(int neuesLevel)
    {
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
                prefab = towerLevel3Prefab;
                level = 3;
                break;
        }

        aktuellerTower = Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );
    }
}