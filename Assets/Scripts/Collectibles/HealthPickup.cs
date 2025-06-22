using UnityEngine;

/// <summary>
/// Represents a collectible health pickup in the game.
/// </summary>
public class HealthPickup : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      // Optional: play collect sound or animation here
      Debug.Log("Health collected by Player!");

      // Destroy the health pickup object
      Destroy(gameObject);
    }
  }
}
