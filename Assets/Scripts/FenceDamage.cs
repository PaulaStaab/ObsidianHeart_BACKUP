////////////using System.Collections;
////////////using UnityEngine;

////////////public class FenceDamage : MonoBehaviour
////////////{
////////////    [Header("Health")]
////////////    [SerializeField] private int maxHealth = 2000;
////////////    private int currentHealth;

////////////    [Header("References")]
////////////    [SerializeField] private GameObject intactFence;
////////////    [SerializeField] private GameObject brokenFence;
////////////    [SerializeField] private Collider intactCollider;
////////////    [SerializeField] private ParticleSystem smokeFx;

////////////    [Header("Death Sequence")]
////////////    [SerializeField] private float flickerDuration = 0.4f;
////////////    [SerializeField] private float dissolveDuration = 1.2f;

////////////    [Header("Dissolve")]
////////////    [SerializeField] private string dissolveProperty = "_DissolveAmount";
////////////    private Renderer[] intactRenderers;
////////////    private MaterialPropertyBlock mpb;

////////////    private bool isDestroyed = false;

////////////    private void Awake()
////////////    {
////////////        currentHealth = maxHealth;

////////////        if (intactFence != null) intactFence.SetActive(true);
////////////        if (brokenFence != null) brokenFence.SetActive(false);

////////////        if (intactFence != null)
////////////            intactRenderers = intactFence.GetComponentsInChildren<Renderer>(true);

////////////        mpb = new MaterialPropertyBlock();
////////////        SetDissolve(0f);
////////////    }

////////////    public void TakeDamage(int damage)
////////////    {
////////////        if (isDestroyed) return;

////////////        currentHealth -= damage;

////////////        if (currentHealth <= 0)
////////////        {
////////////            currentHealth = 0;
////////////            StartCoroutine(DestroySequence());
////////////        }
////////////    }

////////////    private IEnumerator DestroySequence()
////////////    {
////////////        isDestroyed = true;

////////////        if (intactCollider != null)
////////////            intactCollider.enabled = false;

////////////        float timer = 0f;
////////////        bool visible = true;

////////////        while (timer < flickerDuration)
////////////        {
////////////            visible = !visible;
////////////            if (intactFence != null) intactFence.SetActive(visible);
////////////            yield return new WaitForSeconds(0.08f);
////////////            timer += 0.08f;
////////////        }

////////////        if (intactFence != null) intactFence.SetActive(true);

////////////        float elapsed = 0f;
////////////        while (elapsed < dissolveDuration)
////////////        {
////////////            elapsed += Time.deltaTime;
////////////            float t = Mathf.Clamp01(elapsed / dissolveDuration);
////////////            SetDissolve(t);
////////////            yield return null;
////////////        }

////////////        if (smokeFx != null)
////////////            smokeFx.Play();

////////////        if (intactFence != null) intactFence.SetActive(false);
////////////        if (brokenFence != null) brokenFence.SetActive(true);
////////////    }

////////////    private void SetDissolve(float value)
////////////    {
////////////        if (intactRenderers == null) return;

////////////        foreach (var rend in intactRenderers)
////////////        {
////////////            rend.GetPropertyBlock(mpb);
////////////            mpb.SetFloat(dissolveProperty, value);
////////////            rend.SetPropertyBlock(mpb);
////////////        }
////////////    }
////////////}

//////////using UnityEngine;

//////////public class FenceDamage : MonoBehaviour
//////////{
//////////    [Header("Fence References")]
//////////    [SerializeField] private GameObject intactFence;
//////////    [SerializeField] private GameObject damagedFence;
//////////    [SerializeField] private GameObject triggerObject;

//////////    [Header("Shots")]
//////////    [SerializeField] private int shotsToBreak = 10;
//////////    [SerializeField] private string damagingTag = "Projectile";

//////////    private int currentShots = 0;
//////////    private bool isBroken = false;

//////////    private void Awake()
//////////    {
//////////        if (intactFence != null)
//////////            intactFence.SetActive(true);

//////////        if (damagedFence != null)
//////////            damagedFence.SetActive(false);
//////////    }

//////////    private void OnTriggerEnter(Collider other)
//////////    {
//////////        if (isBroken) return;

//////////        if (!other.CompareTag(damagingTag)) return;

//////////        currentShots++;

//////////        if (currentShots >= shotsToBreak)
//////////        {
//////////            BreakFence();
//////////        }
//////////    }

//////////    private void BreakFence()
//////////    {
//////////        isBroken = true;

//////////        if (intactFence != null)
//////////            intactFence.SetActive(false);

//////////        if (damagedFence != null)
//////////            damagedFence.SetActive(true);

//////////        if (triggerObject != null)
//////////            Destroy(triggerObject);
//////////    }
//////////}

////////using UnityEngine;

////////public class FenceDamage : MonoBehaviour
////////{
////////    [Header("Fence References")]
////////    [SerializeField] private GameObject intactFence;
////////    [SerializeField] private GameObject damagedFence;
////////    [SerializeField] private GameObject triggerObject;

////////    [Header("Shots")]
////////    [SerializeField] private int shotsToBreak = 10;
////////    [SerializeField] private string damagingTag = "Projectile";

////////    [Header("Debug")]
////////    [SerializeField] private bool breakNow;   // Häkchen im Inspector

////////    private int currentShots = 0;
////////    private bool isBroken = false;

////////    private void Start()
////////    {
////////        if (intactFence != null)
////////            intactFence.SetActive(true);

////////        if (damagedFence != null)
////////            damagedFence.SetActive(false);
////////    }

////////    private void Update()
////////    {
////////        if (breakNow && !isBroken)
////////        {
////////            breakNow = false;
////////            BreakFence();
////////        }
////////    }

////////    private void OnTriggerEnter(Collider other)
////////    {
////////        if (isBroken) return;
////////        if (!other.CompareTag(damagingTag)) return;

////////        currentShots++;

////////        if (currentShots >= shotsToBreak)
////////        {
////////            BreakFence();
////////        }
////////    }

////////    private void BreakFence()
////////    {
////////        isBroken = true;

////////        if (intactFence != null)
////////            intactFence.SetActive(false);

////////        if (damagedFence != null)
////////            damagedFence.SetActive(true);

////////        if (triggerObject != null)
////////            Destroy(triggerObject);
////////    }
////////}

//////using UnityEngine;

//////public class FenceDamage : MonoBehaviour
//////{
//////    [Header("Fence References")]
//////    [SerializeField] private GameObject intactFence;
//////    [SerializeField] private GameObject damagedFence;
//////    [SerializeField] private GameObject triggerObject;

//////    [Header("FX")]
//////    [SerializeField] private ParticleSystem smokeFx;

//////    [Header("Shots")]
//////    [SerializeField] private int shotsToBreak = 10;
//////    [SerializeField] private string damagingTag = "Projectile";

//////    [Header("Debug")]
//////    [SerializeField] private bool breakNow;

//////    private int currentShots = 0;
//////    private bool isBroken = false;

//////    private void Start()
//////    {
//////        if (intactFence != null)
//////            intactFence.SetActive(true);

//////        if (damagedFence != null)
//////            damagedFence.SetActive(false);

//////        if (smokeFx != null)
//////            smokeFx.gameObject.SetActive(false);
//////    }

//////    private void Update()
//////    {
//////        if (breakNow && !isBroken)
//////        {
//////            breakNow = false;
//////            BreakFence();
//////        }
//////    }

//////    private void OnTriggerEnter(Collider other)
//////    {
//////        if (isBroken) return;
//////        if (!other.CompareTag(damagingTag)) return;

//////        currentShots++;

//////        if (currentShots >= shotsToBreak)
//////        {
//////            BreakFence();
//////        }
//////    }

//////    private void BreakFence()
//////    {
//////        isBroken = true;

//////        if (intactFence != null)
//////            intactFence.SetActive(false);

//////        if (damagedFence != null)
//////            damagedFence.SetActive(true);

//////        if (smokeFx != null)
//////        {
//////            smokeFx.gameObject.SetActive(true);
//////            smokeFx.Play();
//////        }

//////        if (triggerObject != null)
//////            Destroy(triggerObject);
//////    }
//////}

////using UnityEngine;

////public class FenceDamage : MonoBehaviour
////{
////    [Header("Fence References")]
////    [SerializeField] private GameObject intactFence;
////    [SerializeField] private GameObject damagedFence;
////    [SerializeField] private GameObject triggerObject;

////    [Header("FX")]
////    [SerializeField] private ParticleSystem smokeFx;

////    [Header("Shots")]
////    [SerializeField] private int shotsToBreak = 3;

////    [Header("Debug")]
////    [SerializeField] private bool breakNow;

////    private int currentShots = 0;
////    private bool isBroken = false;

////    private void Start()
////    {
////        if (intactFence != null)
////            intactFence.SetActive(true);

////        if (damagedFence != null)
////            damagedFence.SetActive(false);

////        if (smokeFx != null)
////            smokeFx.gameObject.SetActive(false);
////    }

////    private void Update()
////    {
////        if (breakNow && !isBroken)
////        {
////            breakNow = false;
////            BreakFence();
////        }
////    }

////    public void RegisterHit()
////    {
////        if (isBroken) return;

////        currentShots++;

////        if (currentShots >= shotsToBreak)
////        {
////            BreakFence();
////        }
////    }

////    private void BreakFence()
////    {
////        isBroken = true;

////        if (intactFence != null)
////            intactFence.SetActive(false);

////        if (damagedFence != null)
////            damagedFence.SetActive(true);

////        if (smokeFx != null)
////        {
////            smokeFx.gameObject.SetActive(true);
////            smokeFx.Play();
////        }

////        if (triggerObject != null)
////            Destroy(triggerObject);
////    }
////}

//using UnityEngine;

//public class FenceDamage : MonoBehaviour
//{
//    [Header("Fence References")]
//    [SerializeField] private GameObject intactFence;
//    [SerializeField] private GameObject damagedFence;
//    [SerializeField] private GameObject triggerObject;

//    [Header("FX")]
//    [SerializeField] private ParticleSystem smokeFx;

//    [Header("Shots")]
//    [SerializeField] private int shotsToBreak = 10;

//    [Header("Debug")]
//    [SerializeField] private bool breakNow = false;
//    [SerializeField] private bool enableLogs = true;

//    private int currentShots = 0;
//    private bool isBroken = false;

//    private void Start()
//    {
//        if (intactFence != null)
//        {
//            intactFence.SetActive(true);
//            Log("Intakter Zaun aktiviert: " + intactFence.name);
//        }
//        else
//        {
//            Log("WARNUNG: IntactFence ist nicht gesetzt.");
//        }

//        if (damagedFence != null)
//        {
//            damagedFence.SetActive(false);
//            Log("Damaged-Zaun deaktiviert: " + damagedFence.name);
//        }
//        else
//        {
//            Log("WARNUNG: DamagedFence ist nicht gesetzt.");
//        }

//        if (smokeFx != null)
//        {
//            smokeFx.gameObject.SetActive(false);
//            Log("SmokeFX deaktiviert: " + smokeFx.name);
//        }
//        else
//        {
//            Log("SmokeFX ist nicht gesetzt.");
//        }

//        if (triggerObject != null)
//        {
//            Log("TriggerObject gesetzt: " + triggerObject.name);
//        }
//        else
//        {
//            Log("TriggerObject ist nicht gesetzt.");
//        }

//        Log("FenceDamage gestartet. Treffer bis Zerstörung: " + shotsToBreak);
//    }

//    private void Update()
//    {
//        if (breakNow && !isBroken)
//        {
//            breakNow = false;
//            Log("Debug-Häkchen ausgelöst -> BreakFence()");
//            BreakFence();
//        }
//    }

//    public void RegisterHit()
//    {
//        if (isBroken)
//        {
//            Log("Treffer ignoriert, Zaun ist bereits kaputt.");
//            return;
//        }

//        currentShots++;
//        Log("Zaun wurde getroffen. Trefferstand: " + currentShots + " / " + shotsToBreak);

//        if (currentShots >= shotsToBreak)
//        {
//            Log("Trefferlimit erreicht. Zaun wird umgeschaltet.");
//            BreakFence();
//        }
//    }

//    private void BreakFence()
//    {
//        if (isBroken)
//        {
//            Log("BreakFence() ignoriert, Zaun ist bereits kaputt.");
//            return;
//        }

//        isBroken = true;

//        Log("BreakFence() gestartet.");

//        if (intactFence != null)
//        {
//            intactFence.SetActive(false);
//            Log("Intakter Zaun deaktiviert: " + intactFence.name);
//        }

//        if (damagedFence != null)
//        {
//            damagedFence.SetActive(true);
//            Log("Damaged-Zaun aktiviert: " + damagedFence.name);
//        }

//        if (smokeFx != null)
//        {
//            smokeFx.gameObject.SetActive(true);
//            smokeFx.Play();
//            Log("Raucheffekt aktiviert und abgespielt: " + smokeFx.name);
//        }

//        if (triggerObject != null)
//        {
//            Log("TriggerObject wird zerstört: " + triggerObject.name);
//            Destroy(triggerObject);
//        }
//        else
//        {
//            Log("Kein TriggerObject zum Zerstören gesetzt.");
//        }
//    }

//    private void Log(string message)
//    {
//        if (!enableLogs) return;
//        Debug.Log("[FenceDamage] " + message, gameObject);
//    }
//}

using UnityEngine;

public class FenceDamage : MonoBehaviour
{
    [Header("Fence References")]
    [SerializeField] private GameObject intactFence;
    [SerializeField] private GameObject damagedFence;
    [SerializeField] private GameObject triggerObject;

    [Header("FX")]
    [SerializeField] private ParticleSystem smokeFx;

    [Header("Shots")]
    [SerializeField] private int shotsToBreak = 10;
    [SerializeField] private string projectileTag = "Projectile";

    [Header("Debug")]
    [SerializeField] private bool breakNow = false;
    [SerializeField] private bool enableLogs = true;

    private int currentShots = 0;
    private bool isBroken = false;

    private void Start()
    {
        if (intactFence != null)
        {
            intactFence.SetActive(true);
            Log("Intakter Zaun aktiviert: " + intactFence.name);
        }
        else
        {
            Log("WARNUNG: IntactFence ist nicht gesetzt.");
        }

        if (damagedFence != null)
        {
            damagedFence.SetActive(false);
            Log("Damaged-Zaun deaktiviert: " + damagedFence.name);
        }
        else
        {
            Log("WARNUNG: DamagedFence ist nicht gesetzt.");
        }

        if (smokeFx != null)
        {
            smokeFx.gameObject.SetActive(false);
            Log("SmokeFX deaktiviert: " + smokeFx.name);
        }
        else
        {
            Log("SmokeFX ist nicht gesetzt.");
        }

        if (triggerObject != null)
        {
            Log("TriggerObject gesetzt: " + triggerObject.name);
        }
        else
        {
            Log("TriggerObject ist nicht gesetzt.");
        }

        Log("FenceDamage gestartet. Treffer bis Zerstörung: " + shotsToBreak);
        Log("Erwarteter Projectile-Tag: " + projectileTag);
    }

    private void Update()
    {
        if (breakNow && !isBroken)
        {
            breakNow = false;
            Log("Debug-Häkchen ausgelöst -> BreakFence()");
            BreakFence();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Log("OnTriggerEnter mit: " + other.name + " | Tag: " + other.tag);

        if (isBroken)
        {
            Log("Treffer ignoriert, Zaun ist bereits kaputt.");
            return;
        }

        if (!other.CompareTag(projectileTag))
        {
            Log("Treffer ignoriert, da Tag nicht '" + projectileTag + "' ist.");
            return;
        }

        Log("Projectile erkannt -> Treffer wird gezählt.");
        RegisterHit();
    }

    public void RegisterHit()
    {
        if (isBroken)
        {
            Log("RegisterHit ignoriert, Zaun ist bereits kaputt.");
            return;
        }

        currentShots++;
        Log("Zaun wurde getroffen. Trefferstand: " + currentShots + " / " + shotsToBreak);

        if (currentShots >= shotsToBreak)
        {
            Log("Trefferlimit erreicht. Zaun wird umgeschaltet.");
            BreakFence();
        }
    }

    private void BreakFence()
    {
        if (isBroken)
        {
            Log("BreakFence() ignoriert, Zaun ist bereits kaputt.");
            return;
        }

        isBroken = true;
        Log("BreakFence() gestartet.");

        if (intactFence != null)
        {
            intactFence.SetActive(false);
            Log("Intakter Zaun deaktiviert: " + intactFence.name);
        }

        if (damagedFence != null)
        {
            damagedFence.SetActive(true);
            Log("Damaged-Zaun aktiviert: " + damagedFence.name);
        }

        if (smokeFx != null)
        {
            smokeFx.gameObject.SetActive(true);
            smokeFx.Play();
            Log("Raucheffekt aktiviert und abgespielt: " + smokeFx.name);
        }

        if (triggerObject != null)
        {
            Log("TriggerObject wird zerstört: " + triggerObject.name);
            Destroy(triggerObject);
        }
        else
        {
            Log("Kein TriggerObject zum Zerstören gesetzt.");
        }
    }

    private void Log(string message)
    {
        if (!enableLogs) return;
        Debug.Log("[FenceDamage] " + message, gameObject);
    }
}