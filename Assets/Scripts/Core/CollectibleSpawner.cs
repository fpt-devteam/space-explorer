using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns collectibles at random positions and intervals.
/// </summary>
public class CollectibleSpawner : MonoBehaviour
{
  [SerializeField] private GameObject[] collectiblePrefabs;
  private void Start()
  {
    if (collectiblePrefabs == null || collectiblePrefabs.Length == 0)
    {
      Debug.LogWarning("Collectible prefabs are not assigned.");
    }
  }

  public void SpawnCollectibleAt(Vector2 position)
  {
    if (collectiblePrefabs != null && collectiblePrefabs.Length > 0)
    {
      int index = Random.Range(0, collectiblePrefabs.Length);

      if (collectiblePrefabs[index] == null)
      {
        Debug.LogWarning("Collectible prefab is null at index: " + index);
        return;
      }

      Instantiate(collectiblePrefabs[index], position, Quaternion.identity);
    }
  }
}
