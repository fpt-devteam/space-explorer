using UnityEngine;

/// <summary>
/// Represents a collectible shield pickup in the game.
/// </summary>
public class ShieldPickup : CollectibleObject
{
  [SerializeField] private int shieldAmount = 1;

  public override void Collect(GameObject collector)
  {
    var playerShield = collector.GetComponent<Player>();
    if (playerShield != null)
    {
      playerShield.currentShield += shieldAmount;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("Shield collected by Player!");
      Collect(other.gameObject);
      Destroy(gameObject);
    }
  }
}
