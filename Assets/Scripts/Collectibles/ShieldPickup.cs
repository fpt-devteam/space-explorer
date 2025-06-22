using UnityEngine;

/// <summary>
/// Represents a collectible shield pickup in the game.
/// </summary>
public class ShieldPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optional: play collect sound or animation here
            Debug.Log("Shield collected by Player!");

            // Destroy the shield pickup object
            Destroy(gameObject);
        }
    }
}
