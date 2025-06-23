using UnityEngine;

/// <summary>
/// Represents a collectible star in the game.
/// </summary>
public class StarPickup : CollectibleObject
{
  [SerializeField] private int starAmount = 1;

  public override void Collect(GameObject collector)
  {
    var playerStar = collector.GetComponent<Player>();
    
    if (playerStar != null)
    {
      // playerStar.currentHealth += starAmount;
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      Debug.Log("Star collected by Player!");
      StarManager.Instance.AddPoints(1); 
      Collect(other.gameObject);
      Destroy(gameObject);
    }
  }
}
