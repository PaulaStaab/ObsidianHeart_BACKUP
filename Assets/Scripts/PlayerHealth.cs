//using UnityEngine;

//public class PlayerHealth : MonoBehaviour
//{
//    [SerializeField] private int maxHealth = 100;

//    public int CurrentHealth { get; private set; }

//    private void Awake()
//    {
//        CurrentHealth = maxHealth;
//    }

//    public void TakeDamage(int damage)
//    {
//        CurrentHealth -= damage;
//        CurrentHealth = Mathf.Max(CurrentHealth, 0);

//        Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");

//        if (CurrentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        Debug.Log("Player died!");
//        // Hier später Death-Animation, Respawn oder Game Over einbauen
//    }
//}

using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText;

    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        UpdateHealthUI();

        Debug.Log($"Player took {damage} damage. Current health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {CurrentHealth} / {maxHealth}";
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
    }
}