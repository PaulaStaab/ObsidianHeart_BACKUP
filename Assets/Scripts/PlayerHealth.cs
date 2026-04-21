////////////////using UnityEngine;

////////////////public class PlayerHealth : MonoBehaviour
////////////////{
////////////////    [SerializeField] private int maxHealth = 100;

////////////////    public int CurrentHealth { get; private set; }

////////////////    private void Awake()
////////////////    {
////////////////        CurrentHealth = maxHealth;
////////////////    }

////////////////    public void TakeDamage(int damage)
////////////////    {
////////////////        CurrentHealth -= damage;
////////////////        CurrentHealth = Mathf.Max(CurrentHealth, 0);

////////////////        Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");

////////////////        if (CurrentHealth <= 0)
////////////////        {
////////////////            Die();
////////////////        }
////////////////    }

////////////////    private void Die()
////////////////    {
////////////////        Debug.Log("Player died!");
////////////////        // Hier später Death-Animation, Respawn oder Game Over einbauen
////////////////    }
////////////////}

//////////////using UnityEngine;
//////////////using TMPro;

//////////////public class PlayerHealth : MonoBehaviour
//////////////{
//////////////    [Header("Health")]
//////////////    [SerializeField] private int maxHealth = 100;

//////////////    [Header("UI")]
//////////////    [SerializeField] private TextMeshProUGUI healthText;

//////////////    public int CurrentHealth { get; private set; }

//////////////    private void Awake()
//////////////    {
//////////////        CurrentHealth = maxHealth;
//////////////        UpdateHealthUI();
//////////////    }

//////////////    public void TakeDamage(int damage)
//////////////    {
//////////////        CurrentHealth -= damage;
//////////////        CurrentHealth = Mathf.Max(CurrentHealth, 0);

//////////////        UpdateHealthUI();

//////////////        Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");

//////////////        if (CurrentHealth <= 0)
//////////////        {
//////////////            Die();
//////////////        }
//////////////    }

//////////////    private void UpdateHealthUI()
//////////////    {
//////////////        if (healthText != null)
//////////////        {
//////////////            healthText.text = $"HP: {CurrentHealth} / {maxHealth}";
//////////////        }
//////////////    }

//////////////    private void Die()
//////////////    {
//////////////        Debug.Log("Player died!");
//////////////    }
//////////////}

////////////using UnityEngine;
////////////using TMPro;

////////////public class PlayerHealth : MonoBehaviour
////////////{
////////////    [Header("Health")]
////////////    [SerializeField] private int maxHealth = 100;

////////////    [Header("UI")]
////////////    [SerializeField] private TextMeshProUGUI healthText;

////////////    public int CurrentHealth { get; private set; }

////////////    private void Start()
////////////    {
////////////        CurrentHealth = maxHealth;
////////////        UpdateHealthUI();
////////////    }

////////////    public void TakeDamage(int damage)
////////////    {
////////////        CurrentHealth -= damage;
////////////        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

////////////        UpdateHealthUI();

////////////        if (CurrentHealth <= 0)
////////////        {
////////////            Die();
////////////        }
////////////    }

////////////    private void UpdateHealthUI()
////////////    {
////////////        if (healthText != null)
////////////        {
////////////            healthText.text = "HP: " + CurrentHealth + " / " + maxHealth;
////////////        }
////////////    }

////////////    private void Die()
////////////    {
////////////        Debug.Log("Player died!");
////////////    }
////////////}

//////using UnityEngine;
//////using TMPro;

//////public class PlayerHealth : MonoBehaviour
//////{
//////    [Header("Health")]
//////    [SerializeField] private int maxHealth = 100;

//////    [Header("UI")]
//////    [SerializeField] private TextMeshProUGUI healthText;

//////    public int CurrentHealth { get; private set; }

//////    private void Start()
//////    {
//////        CurrentHealth = maxHealth;
//////        UpdateHealthUI();
//////    }

//////    public void TakeDamage(int damage)
//////    {
//////        CurrentHealth -= damage;
//////        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

//////        Debug.Log("Player hat Damage erkannt: " + damage + " | Aktuelle HP: " + CurrentHealth);

//////        UpdateHealthUI();

//////        if (CurrentHealth <= 0)
//////        {
//////            Die();
//////        }
//////    }

//////    private void UpdateHealthUI()
//////    {
//////        if (healthText != null)
//////        {
//////            healthText.text = "HP: " + CurrentHealth + " / " + maxHealth;
//////        }
//////    }

//////    private void Die()
//////    {
//////        Debug.Log("Player died!");
//////        Destroy(gameObject);
//////    }
//////}


////using UnityEngine;
////using TMPro;

////public class PlayerHealth : MonoBehaviour
////{
////    [Header("Health")]
////    [SerializeField] private int maxHealth = 100;

////    [Header("UI")]
////    [SerializeField] private TextMeshProUGUI healthText;

////    [Header("Sound")]
////    [SerializeField] private AudioClip deathSound;

////    public int CurrentHealth { get; private set; }

////    private AudioSource audioSource;

////    private void Awake()
////    {
////        audioSource = GetComponent<AudioSource>();
////    }

////    private void Start()
////    {
////        CurrentHealth = maxHealth;
////        UpdateHealthUI();
////    }

////    public void TakeDamage(int damage)
////    {
////        CurrentHealth -= damage;
////        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

////        Debug.Log("Player hat Damage erkannt: " + damage + " | Aktuelle HP: " + CurrentHealth);

////        UpdateHealthUI();

////        if (CurrentHealth <= 0)
////        {
////            Die();
////        }
////    }

////    private void UpdateHealthUI()
////    {
////        if (healthText != null)
////        {
////            healthText.text = "HP: " + CurrentHealth + " / " + maxHealth;
////        }
////    }

////    private void Die()
////    {
////        Debug.Log("Player died!");

////        if (deathSound != null && audioSource != null)
////        {
////            audioSource.PlayOneShot(deathSound);
////        }

////        Destroy(gameObject);
////    }
////}

using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;

    public int CurrentHealth { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        Debug.Log("Player hat Damage erkannt: " + damage + " | Aktuelle HP: " + CurrentHealth);

        UpdateHealthUI();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + CurrentHealth + " / " + maxHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
            StartCoroutine(DestroyAfterDeathSound());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterDeathSound()
    {
        yield return new WaitForSeconds(deathSound.length);
        Destroy(gameObject);
    }
}

