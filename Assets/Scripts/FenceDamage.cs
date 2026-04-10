using System.Collections;
using UnityEngine;

public class FenceDamage : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 2000;
    private int currentHealth;

    [Header("References")]
    [SerializeField] private GameObject intactFence;
    [SerializeField] private GameObject brokenFence;
    [SerializeField] private Collider intactCollider;
    [SerializeField] private ParticleSystem smokeFx;

    [Header("Death Sequence")]
    [SerializeField] private float flickerDuration = 0.4f;
    [SerializeField] private float dissolveDuration = 1.2f;

    [Header("Dissolve")]
    [SerializeField] private string dissolveProperty = "_DissolveAmount";
    private Renderer[] intactRenderers;
    private MaterialPropertyBlock mpb;

    private bool isDestroyed = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        if (intactFence != null) intactFence.SetActive(true);
        if (brokenFence != null) brokenFence.SetActive(false);

        if (intactFence != null)
            intactRenderers = intactFence.GetComponentsInChildren<Renderer>(true);

        mpb = new MaterialPropertyBlock();
        SetDissolve(0f);
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(DestroySequence());
        }
    }

    private IEnumerator DestroySequence()
    {
        isDestroyed = true;

        if (intactCollider != null)
            intactCollider.enabled = false;

        float timer = 0f;
        bool visible = true;

        while (timer < flickerDuration)
        {
            visible = !visible;
            if (intactFence != null) intactFence.SetActive(visible);
            yield return new WaitForSeconds(0.08f);
            timer += 0.08f;
        }

        if (intactFence != null) intactFence.SetActive(true);

        float elapsed = 0f;
        while (elapsed < dissolveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / dissolveDuration);
            SetDissolve(t);
            yield return null;
        }

        if (smokeFx != null)
            smokeFx.Play();

        if (intactFence != null) intactFence.SetActive(false);
        if (brokenFence != null) brokenFence.SetActive(true);
    }

    private void SetDissolve(float value)
    {
        if (intactRenderers == null) return;

        foreach (var rend in intactRenderers)
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetFloat(dissolveProperty, value);
            rend.SetPropertyBlock(mpb);
        }
    }
}