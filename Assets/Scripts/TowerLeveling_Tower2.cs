using UnityEngine;

// Manages tower leveling system for Tower 2 by activating additional child cannons
// based on the current tower level and spending resources through the resource manager.
public class TowerLeveling_Tower2 : MonoBehaviour
{
    [Header("Level-System")]
    // Current level of the tower.
    public int level = 1;
    // Base upgrade cost, multiplied by the current level.
    public int basisKosten = 20;

    [Header("Kanonen (als Childs dieses Turms)")]
    // Cannon that is active from level 1 onward.
    public GameObject kanone1;
    // Cannon that becomes active at level 2.
    public GameObject kanone2;
    // Cannon that becomes active at level 3.
    public GameObject kanone3;

    void Update()
    {
        // Debug input: press H to attempt an upgrade.
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("H gedrückt bei Tower " + name);
            VersucheUpgrade();
        }
    }

    public void VersucheUpgrade()
    {
        // Stop if the resource manager instance is missing.
        if (RessourcenManager.Instance == null)
        {
            Debug.LogError("RessourcenManager.Instance ist NULL!");
            return;
        }

        // Calculate the upgrade cost based on the current level.
        int kosten = basisKosten * level;
        Debug.Log($"Upgrade: Level={level}, Kosten={kosten}");

        // Try to spend the required resources.
        if (RessourcenManager.Instance.SpendRessource(kosten))
        {
            // Increase the level, but do not go above level 3.
            level = Mathf.Min(level + 1, 3);
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
        // Cannon 1 is always active at level 1 or higher.
        if (kanone1 != null)
        {
            kanone1.SetActive(true);
        }

        // Activate cannon 2 at level 2 or higher.
        if (kanone2 != null)
        {
            kanone2.SetActive(neuesLevel >= 2);
        }

        // Activate cannon 3 at level 3 or higher.
        if (kanone3 != null)
        {
            kanone3.SetActive(neuesLevel >= 3);
        }
    }

    // Can be called from a UI button, for example an upgrade button.
    public void UpgradeButton()
    {
        VersucheUpgrade();
    }
}