using UnityEngine;

/// <summary>
/// Represents a collectible stamina pickup in the game.
/// </summary>
public class StaminaPickup : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      // Optional: play collect sound or animation here
      Debug.Log("Stamina collected by Player!");

      // Destroy the stamina pickup object
      Destroy(gameObject);
    }
  }
}
