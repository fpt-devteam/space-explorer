using UnityEngine;

public class HealthPickup : CollectibleObject
{
  [SerializeField] private int healthAmount = 1;

  public override void Collect(GameObject collector)
  {
    var playerHealth = collector.GetComponent<Player>();
    if (playerHealth != null)
    {
      playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healthAmount, playerHealth.maxHealth);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("Health collected by Player!");
      Collect(other.gameObject);
      Destroy(gameObject);
    }
  }
}
