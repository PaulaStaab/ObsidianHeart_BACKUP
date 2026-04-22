using UnityEngine;

// Requires a ParticleSystem on the same GameObject.
[RequireComponent(typeof(ParticleSystem))]
// Cycles the particle system's start color through a rainbow over time.
public class ParticleRainbow : MonoBehaviour
{
    // Speed at which the hue value changes.
    [SerializeField] private float colorSpeed = 0.2f;
    // Alpha value applied to the generated rainbow color.
    [SerializeField] private float alpha = 1f;

    // Cached reference to the particle system.
    private ParticleSystem ps;
    // Current hue value used for rainbow color generation.
    private float hue = 0f;

    private void Start()
    {
        // Cache the ParticleSystem component on this object.
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        // Advance the hue value over time.
        hue += colorSpeed * Time.deltaTime;

        // Wrap the hue value back into the valid 0 to 1 range.
        if (hue > 1f)
            hue -= 1f;

        // Convert the hue to a fully saturated and bright rainbow color.
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        rainbowColor.a = alpha;

        // Apply the generated color to the particle system's start color.
        var main = ps.main;
        main.startColor = rainbowColor;
    }
}