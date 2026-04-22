using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

// Handles player health, updates the health UI, plays a death sound,
// and loads the game over scene when the player dies.
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    // Maximum health value the player starts with.
    [SerializeField] private int maxHealth = 300;

    [Header("UI")]
    // UI text element used to display the current health.
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Sound")]
    // Optional sound played when the player dies.
    [SerializeField] private AudioClip deathSound;

    [Header("Scene")]
    // Name of the scene that should be loaded after death.
    [SerializeField] private string gameOverSceneName = "GameOver";

    // Current health value, readable from other scripts but only set internally.
    public int CurrentHealth { get; private set; }

    // Cached AudioSource used for the death sound.
    private AudioSource audioSource;

    private void Awake()
    {
        // Cache the AudioSource component on this object.
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Initialize the player's health and update the UI once at startup.
        CurrentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the incoming damage amount.
        CurrentHealth -= damage;
        // Clamp health so it always stays between 0 and maxHealth.
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        // Log the received damage and the player's current remaining health.
        Debug.Log("Player hat Damage erkannt: " + damage + " | Aktuelle HP: " + CurrentHealth);

        // Refresh the health display after taking damage.
        UpdateHealthUI();

        // Trigger death logic when health reaches zero.
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        // Update the UI text if a health label has been assigned.
        if (healthText != null)
        {
            healthText.text = "HP: " + CurrentHealth + " / " + maxHealth;
        }
    }

    private void Die()
    {
        // Write a debug message when the player dies.
        Debug.Log("Player died!");

        // If a death sound and audio source exist, play the sound first and wait before changing scene.
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
            StartCoroutine(DestroyAfterDeathSound());
        }
        else
        {
            // Otherwise destroy the player immediately and load the game over scene.
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator DestroyAfterDeathSound()
    {
        // Wait until the death sound has finished playing.
        yield return new WaitForSeconds(deathSound.length);
        // Destroy the player and then load the game over scene.
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }
}