using UnityEngine;
using UnityEngine.InputSystem;

// Manages tower leveling system and upgrades between tower prefab levels.
public class TowerLeveling_Tower1 : MonoBehaviour
{
    // Current level of this tower instance.
    public int level = 1;

    // Prefab for the tower at level 2.
    public GameObject towerLevel2Prefab;
    // Prefab for the tower at level 3.
    public GameObject towerLevel3Prefab;

    // Currently spawned tower prefab instance.
    private GameObject aktuellerTower;

    // Base cost for tower upgrades.
    public int basisKosten = 20;

    void Update()
    {
        // Debug input: press H to attempt an upgrade.
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
            // Placement logic.
        }
        else
        {
            Debug.Log("Nicht genug Ressourcen für Upgrade!");
        }
    }

    private void SetzeTowerAufLevel(int neuesLevel)
    {
        // Remove the currently spawned tower instance if one exists.
        if (aktuellerTower != null)
        {
            Destroy(aktuellerTower);
        }

        GameObject prefab = null;

        // Select the correct prefab for the target level.
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

        // Spawn the new tower prefab as a child of this object.
        aktuellerTower = Instantiate(
            prefab,
            transform.position,
            transform.rotation,
            transform
        );
    }
}