using UnityEngine;

public class SpiderSpawn : MonoBehaviour
{
    [Header("Spinnen Einstellungen")]
    public GameObject spinnePrefab;

    [Header("Spawn Bereich (Min Koordinaten)")]
    public float minX = -10f;
    public float minY = 0f;
    public float minZ = -10f;

    [Header("Spawn Bereich (Max Koordinaten)")]
    public float maxX = 10f;
    public float maxY = 0f;
    public float maxZ = 10f;

    [Header("Spawn Einstellungen")]
    public int maxSpinnen = 5;
    public float spawnIntervall = 5f;

    private int aktuelleSpinnen = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnSpinne), 1f, spawnIntervall);
    }

    void SpawnSpinne()
    {
        if (aktuelleSpinnen < maxSpinnen && spinnePrefab != null)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                Random.Range(minZ, maxZ)
            );

            Instantiate(spinnePrefab, spawnPos, Quaternion.identity);
            aktuelleSpinnen++;
        }
    }

    // Rufe diese Methode auf, wenn eine Spinne zerstört wird (z.B. über OnDestroy in Spinne-Script)
    public void SpinneZerstört()
    {
        aktuelleSpinnen--;
    }
}
