using UnityEngine;

// Handles fence damage, swaps the intact and damaged fence states,
// plays smoke effects, and removes the trigger object once the fence breaks.
public class FenceDamage : MonoBehaviour
{
    [Header("Fence References")]
    // Reference to the fence object shown before it is broken.
    [SerializeField] private GameObject intactFence;
    // Reference to the fence object shown after it is broken.
    [SerializeField] private GameObject damagedFence;
    // Trigger object that gets destroyed after the fence breaks.
    [SerializeField] private GameObject triggerObject;

    [Header("FX")]
    // Smoke particle effect played when the fence is destroyed.
    [SerializeField] private ParticleSystem smokeFx;

    [Header("Shots")]
    // Number of valid projectile hits required to break the fence.
    [SerializeField] private int shotsToBreak = 10;
    // Tag used to identify projectiles that can damage the fence.
    [SerializeField] private string projectileTag = "Projectile";

    [Header("Debug")]
    // Debug checkbox that forces the fence to break in play mode.
    [SerializeField] private bool breakNow = false;
    // Enables or disables debug log output.
    [SerializeField] private bool enableLogs = true;

    // Current number of registered hits.
    private int currentShots = 0;
    // Tracks whether the fence has already been broken.
    private bool isBroken = false;

    private void Start()
    {
        // Activate the intact fence at the start if it exists.
        if (intactFence != null)
        {
            intactFence.SetActive(true);
            Log("Intact fence enabled: " + intactFence.name);
        }
        else
        {
            Log("WARNING: IntactFence is not assigned.");
        }

        // Hide the damaged fence version at the start if it exists.
        if (damagedFence != null)
        {
            damagedFence.SetActive(false);
            Log("Damaged fence disabled: " + damagedFence.name);
        }
        else
        {
            Log("WARNING: DamagedFence is not assigned.");
        }

        // Disable the smoke effect until the fence is actually broken.
        if (smokeFx != null)
        {
            smokeFx.gameObject.SetActive(false);
            Log("Smoke FX disabled: " + smokeFx.name);
        }
        else
        {
            Log("SmokeFX is not assigned.");
        }

        // Log whether a trigger object is available.
        if (triggerObject != null)
        {
            Log("TriggerObject assigned: " + triggerObject.name);
        }
        else
        {
            Log("TriggerObject is not assigned.");
        }

        // Log startup information for debugging.
        Log("FenceDamage started. Hits required to break: " + shotsToBreak);
        Log("Expected projectile tag: " + projectileTag);
    }

    private void Update()
    {
        // Allow manual breaking through the debug checkbox.
        if (breakNow && !isBroken)
        {
            breakNow = false;
            Log("Debug checkbox triggered -> BreakFence()");
            BreakFence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Log every trigger contact for debugging purposes.
        Log("OnTriggerEnter with: " + other.name + " | Tag: " + other.tag);

        // Ignore further hits if the fence is already broken.
        if (isBroken)
        {
            Log("Hit ignored, fence is already broken.");
            return;
        }

        // Ignore objects that do not have the required projectile tag.
        if (!other.CompareTag(projectileTag))
        {
            Log("Hit ignored because tag is not '" + projectileTag + "'.");
            return;
        }

        // Register a valid projectile hit.
        Log("Projectile detected -> hit will be counted.");
        RegisterHit();
    }

    public void RegisterHit()
    {
        // Prevent hit registration after the fence is already broken.
        if (isBroken)
        {
            Log("RegisterHit ignored, fence is already broken.");
            return;
        }

        // Increase the hit counter.
        currentShots++;
        Log("Fence was hit. Current hits: " + currentShots + " / " + shotsToBreak);

        // Break the fence once the hit threshold is reached.
        if (currentShots >= shotsToBreak)
        {
            Log("Hit limit reached. Fence will now switch state.");
            BreakFence();
        }
    }

    private void BreakFence()
    {
        // Prevent running the break logic more than once.
        if (isBroken)
        {
            Log("BreakFence() ignored, fence is already broken.");
            return;
        }

        isBroken = true;
        Log("BreakFence() started.");

        // Hide the intact fence model.
        if (intactFence != null)
        {
            intactFence.SetActive(false);
            Log("Intact fence disabled: " + intactFence.name);
        }

        // Show the damaged fence model.
        if (damagedFence != null)
        {
            damagedFence.SetActive(true);
            Log("Damaged fence enabled: " + damagedFence.name);
        }

        // Activate and play the smoke effect.
        if (smokeFx != null)
        {
            smokeFx.gameObject.SetActive(true);
            smokeFx.Play();
            Log("Smoke effect enabled and played: " + smokeFx.name);
        }

        // Destroy the trigger object so it can no longer be used.
        if (triggerObject != null)
        {
            Log("TriggerObject will be destroyed: " + triggerObject.name);
            Destroy(triggerObject);
        }
        else
        {
            Log("No TriggerObject assigned for destruction.");
        }
    }

    private void Log(string message)
    {
        // Only write logs when debug logging is enabled.
        if (!enableLogs) return;
        Debug.Log("[FenceDamage] " + message, gameObject);
    }
}