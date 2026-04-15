//using UnityEngine;

//public class SpiderSpawn : MonoBehaviour
//{
//    [Header("Spinnen Einstellungen")]
//    public GameObject spinnePrefab;

//    [Header("Spawn Bereich (Min Koordinaten)")]
//    public float minX = -10f;
//    public float minY = 0f;
//    public float minZ = -10f;

//    [Header("Spawn Bereich (Max Koordinaten)")]
//    public float maxX = 10f;
//    public float maxY = 0f;
//    public float maxZ = 10f;

//    [Header("Spawn Einstellungen")]
//    public int maxSpinnen = 5;
//    public float spawnIntervall = 5f;

//    private int aktuelleSpinnen = 0;

//    void Start()
//    {
//        InvokeRepeating(nameof(SpawnSpinne), 1f, spawnIntervall);
//    }

//    void SpawnSpinne()
//    {
//        if (aktuelleSpinnen < maxSpinnen && spinnePrefab != null)
//        {
//            Vector3 spawnPos = new Vector3(
//                Random.Range(minX, maxX),
//                Random.Range(minY, maxY),
//                Random.Range(minZ, maxZ)
//            );

//            Instantiate(spinnePrefab, spawnPos, Quaternion.identity);
//            aktuelleSpinnen++;
//        }
//    }

//    // Rufe diese Methode auf, wenn eine Spinne zerstört wird (z.B. über OnDestroy in Spinne-Script)
//    public void SpinneZerstört()
//    {
//        aktuelleSpinnen--;
//    }
//}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spinnen Einstellungen")]
    public GameObject spinnePrefab;

    [Header("Spinnen Spawn Bereich (Min Koordinaten)")]
    public float spinneMinX = -10f;
    public float spinneMinY = 0f;
    public float spinneMinZ = -10f;

    [Header("Spinnen Spawn Bereich (Max Koordinaten)")]
    public float spinneMaxX = 10f;
    public float spinneMaxY = 0f;
    public float spinneMaxZ = 10f;

    [Header("Spinnen Spawn Einstellungen")]
    public int maxSpinnen = 5;
    public float spinnenSpawnIntervall = 5f;

    private int aktuelleSpinnen = 0;


    [Header("Monster Einstellungen")]
    public GameObject monsterPrefab;

    [Header("Monster Spawn Bereich (Min Koordinaten)")]
    public float monsterMinX = -10f;
    public float monsterMinY = 0f;
    public float monsterMinZ = -10f;

    [Header("Monster Spawn Bereich (Max Koordinaten)")]
    public float monsterMaxX = 10f;
    public float monsterMaxY = 0f;
    public float monsterMaxZ = 10f;

    [Header("Monster Spawn Einstellungen")]
    public int maxMonster = 5;
    public float monsterSpawnIntervall = 5f;

    private int aktuelleMonster = 0;


    [Header("Creature Einstellungen")]
    public GameObject creaturePrefab;

    [Header("Creature Spawn Bereich (Min Koordinaten)")]
    public float creatureMinX = -10f;
    public float creatureMinY = 0f;
    public float creatureMinZ = -10f;

    [Header("Creature Spawn Bereich (Max Koordinaten)")]
    public float creatureMaxX = 10f;
    public float creatureMaxY = 0f;
    public float creatureMaxZ = 10f;

    [Header("Creature Spawn Einstellungen")]
    public int maxCreatures = 5;
    public float creatureSpawnIntervall = 5f;

    private int aktuelleCreatures = 0;


    void Start()
    {
        InvokeRepeating(nameof(SpawnSpinne), 1f, spinnenSpawnIntervall);
        InvokeRepeating(nameof(SpawnMonster), 1f, monsterSpawnIntervall);
        InvokeRepeating(nameof(SpawnCreature), 1f, creatureSpawnIntervall);
    }

    void SpawnSpinne()
    {
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
        aktuelleSpinnen--;
    }

    public void MonsterZerstoert()
    {
        aktuelleMonster--;
    }

    public void CreatureZerstoert()
    {
        aktuelleCreatures--;
    }
}
