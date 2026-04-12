//using System.Collections;
//using UnityEngine;

//public class SmokeFade : MonoBehaviour
//{
//    [Header("References")]
//    [SerializeField] private ParticleSystem smoke;

//    [Header("Timing")]
//    [SerializeField] private float fadeDelay = 2f;

//    [Header("Cleanup")]
//    [SerializeField] private bool disableAfterStop = true;

//    private void Reset()
//    {
//        if (smoke == null)
//            smoke = GetComponent<ParticleSystem>();
//    }

//    public void PlaySmoke()
//    {
//        if (smoke == null) return;

//        gameObject.SetActive(true);
//        smoke.Play();
//        StopAllCoroutines();
//        StartCoroutine(FadeRoutine());
//    }

//    private IEnumerator FadeRoutine()
//    {
//        yield return new WaitForSeconds(fadeDelay);

//        var emission = smoke.emission;
//        emission.enabled = false;

//        while (smoke.IsAlive(true))
//            yield return null;

//        if (disableAfterStop)
//            gameObject.SetActive(false);
//    }
//}

using System.Collections;
using UnityEngine;

public class SmokeFade : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ParticleSystem smoke;

    [Header("Timing")]
    [SerializeField] private float visibleTime = 2f;
    [SerializeField] private float fadeDuration = 1.5f;

    [Header("Cleanup")]
    [SerializeField] private bool disableAfterFade = true;

    private ParticleSystem.EmissionModule emission;
    private float startRate;

    private void Awake()
    {
        if (smoke == null)
            smoke = GetComponent<ParticleSystem>();

        emission = smoke.emission;
        startRate = emission.rateOverTimeMultiplier;
    }

    public void PlaySmoke()
    {
        if (smoke == null) return;

        gameObject.SetActive(true);

        emission = smoke.emission;
        emission.enabled = true;
        emission.rateOverTimeMultiplier = startRate;

        smoke.Play();
        StopAllCoroutines();
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(visibleTime);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / fadeDuration);
            emission.rateOverTimeMultiplier = Mathf.Lerp(startRate, 0f, normalized);
            yield return null;
        }

        emission.rateOverTimeMultiplier = 0f;

        while (smoke.IsAlive(true))
            yield return null;

        if (disableAfterFade)
            gameObject.SetActive(false);
    }
}