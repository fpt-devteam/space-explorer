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
      playerShield.currentShield = Mathf.Min(playerShield.currentShield + shieldAmount, playerShield.maxShield);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      SoundManager.Instance.PlaySFX(SoundManager.Instance.hitShield);
      Collect(other.gameObject);
      Destroy(gameObject);
    }
  }
}
