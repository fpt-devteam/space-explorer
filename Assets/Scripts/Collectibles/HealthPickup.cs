using UnityEngine;

public class HealthPickup : MonoBehaviour
{
  [SerializeField] private int healthAmount = 25;

  public void Collect(GameObject collector)
  {
    var playerHealth = collector.GetComponent<Player>();
    if (playerHealth != null)
    {
      playerHealth.currentHealth += healthAmount;
    }
  }

  /// <summary>
  /// Handles collision with the player or other collectors.
  /// </summary>
  /// <param name="other">The collider that entered the trigger.</param>
  private void OnTriggerEnter(Collider other)
  {
    // Check if the colliding object is the player
    if (other.CompareTag("Player"))
    {
      Collect(other.gameObject);
    }
  }
}
