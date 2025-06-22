using UnityEngine;

/// <summary>
/// Represents a collectible star in the game.
/// </summary>
public class StarPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: play collect sound or animation here
            Debug.Log("Star collected by Player!");

            // Destroy the star object
            Destroy(gameObject);
        }
    }
}
