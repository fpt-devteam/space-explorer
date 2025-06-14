using UnityEngine;

/// <summary>
/// Handles player health and triggers game over.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage = 1)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        Debug.Log($"Player took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount = 1)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        Debug.Log($"Player healed {amount}. Health: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        Debug.Log("Player died!");

        // Trigger game over
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }

        // Disable player
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            TakeDamage(1);
            // Optional: Destroy the asteroid
            Destroy(collision.gameObject);
        }
    }
}
