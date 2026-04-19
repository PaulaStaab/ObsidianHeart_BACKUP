using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleRainbow : MonoBehaviour
{
    [SerializeField] private float colorSpeed = 0.2f;
    [SerializeField] private float alpha = 1f;

    private ParticleSystem ps;
    private float hue = 0f;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        hue += colorSpeed * Time.deltaTime;

        if (hue > 1f)
            hue -= 1f;

        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
        rainbowColor.a = alpha;

        var main = ps.main;
        main.startColor = rainbowColor;
    }
}