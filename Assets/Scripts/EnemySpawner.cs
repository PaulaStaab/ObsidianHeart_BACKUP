using UnityEngine;

// Spawns three different enemy types inside their own configurable areas
// and keeps track of how many of each type are currently active.
public class EnemySpawner : MonoBehaviour
{
    [Header("Spinnen Einstellungen")]
    // Prefab for the spider enemy.
    public GameObject spinnePrefab;

    [Header("Spinnen Spawn Bereich (Min Koordinaten)")]
    // Minimum spawn coordinates for spiders.
    public float spinneMinX = -10f;
    public float spinneMinY = 0f;
    public float spinneMinZ = -10f;

    [Header("Spinnen Spawn Bereich (Max Koordinaten)")]
    // Maximum spawn coordinates for spiders.
    public float spinneMaxX = 10f;
    public float spinneMaxY = 0f;
    public float spinneMaxZ = 10f;

    [Header("Spinnen Spawn Einstellungen")]
    // Maximum number of spiders allowed at the same time.
    public int maxSpinnen = 5;
    // Time interval between spider spawn attempts.
    public float spinnenSpawnIntervall = 5f;

    // Current number of active spiders.
    private int aktuelleSpinnen = 0;

    [Header("Monster Einstellungen")]
    // Prefab for the monster enemy.
    public GameObject monsterPrefab;

    [Header("Monster Spawn Bereich (Min Koordinaten)")]
    // Minimum spawn coordinates for monsters.
    public float monsterMinX = -10f;
    public float monsterMinY = 0f;
    public float monsterMinZ = -10f;

    [Header("Monster Spawn Bereich (Max Koordinaten)")]
    // Maximum spawn coordinates for monsters.
    public float monsterMaxX = 10f;
    public float monsterMaxY = 0f;
    public float monsterMaxZ = 10f;

    [Header("Monster Spawn Einstellungen")]
    // Maximum number of monsters allowed at the same time.
    public int maxMonster = 5;
    // Time interval between monster spawn attempts.
    public float monsterSpawnIntervall = 5f;

    // Current number of active monsters.
    private int aktuelleMonster = 0;

    [Header("Creature Einstellungen")]
    // Prefab for the creature enemy.
    public GameObject creaturePrefab;

    [Header("Creature Spawn Bereich (Min Koordinaten)")]
    // Minimum spawn coordinates for creatures.
    public float creatureMinX = -10f;
    public float creatureMinY = 0f;
    public float creatureMinZ = -10f;

    [Header("Creature Spawn Bereich (Max Koordinaten)")]
    // Maximum spawn coordinates for creatures.
    public float creatureMaxX = 10f;
    public float creatureMaxY = 0f;
    public float creatureMaxZ = 10f;

    [Header("Creature Spawn Einstellungen")]
    // Maximum number of creatures allowed at the same time.
    public int maxCreatures = 5;
    // Time interval between creature spawn attempts.
    public float creatureSpawnIntervall = 5f;

    // Current number of active creatures.
    private int aktuelleCreatures = 0;

    void Start()
    {
        // Start repeated spawning for all three enemy types.
        InvokeRepeating(nameof(SpawnSpinne), 1f, spinnenSpawnIntervall);
        InvokeRepeating(nameof(SpawnMonster), 1f, monsterSpawnIntervall);
        InvokeRepeating(nameof(SpawnCreature), 1f, creatureSpawnIntervall);
    }

    void SpawnSpinne()
    {
        // Spawn a spider only if the current count is below the limit and a prefab exists.
        if (aktuelleSpinnen < maxSpinnen && spinnePrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(spinneMinX, spinneMaxX),
                Random.Range(spinneMinY, spinneMaxY),
                Random.Range(spinneMinZ, spinneMaxZ)
            );

            Instantiate(spinnePrefab, spawnPos, Quaternion.identity);
            aktuelleSpinnen++;
        }
    }

    void SpawnMonster()
    {
        // Spawn a monster only if the current count is below the limit and a prefab exists.
        if (aktuelleMonster < maxMonster && monsterPrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(monsterMinX, monsterMaxX),
                Random.Range(monsterMinY, monsterMaxY),
                Random.Range(monsterMinZ, monsterMaxZ)
            );

            Instantiate(monsterPrefab, spawnPos, Quaternion.identity);
            aktuelleMonster++;
        }
    }

    void SpawnCreature()
    {
        // Spawn a creature only if the current count is below the limit and a prefab exists.
        if (aktuelleCreatures < maxCreatures && creaturePrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(creatureMinX, creatureMaxX),
                Random.Range(creatureMinY, creatureMaxY),
                Random.Range(creatureMinZ, creatureMaxZ)
            );

            Instantiate(creaturePrefab, spawnPos, Quaternion.identity);
            aktuelleCreatures++;
        }
    }

    public void SpinneZerstoert()
    {
        // Decrease the active spider count when one is destroyed.
        aktuelleSpinnen--;
    }

    public void MonsterZerstoert()
    {
        // Decrease the active monster count when one is destroyed.
        aktuelleMonster--;
    }

    public void CreatureZerstoert()
    {
        // Decrease the active creature count when one is destroyed.
        aktuelleCreatures--;
    }
}