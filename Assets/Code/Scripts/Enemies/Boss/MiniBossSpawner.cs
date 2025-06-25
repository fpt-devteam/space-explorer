using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns and manages mini bosses for the main boss
/// </summary>
public class MiniBossSpawner : MonoBehaviour
{
  [Header("Spawning Settings")]
  [SerializeField] private GameObject miniBossPrefab;
  [SerializeField] private float spawnRadius = 4f;
  [SerializeField] private int maxMiniBosses = 3;

  [Header("Phase Settings")]
  [SerializeField] private float phase1SpawnInterval = 15f; // Guard Protocol
  [SerializeField] private float phase2SpawnInterval = 12f; // Chaotic Overload
  [SerializeField] private float phase3SpawnInterval = 8f;  // Core Awakening

  private List<GameObject> activeMiniBosses = new List<GameObject>();
  private float lastSpawnTime;
  private Boss mainBoss;

  public int ActiveMiniBossCount => activeMiniBosses.Count;
  public int MaxMiniBosses => maxMiniBosses;

  private void Awake()
  {
    mainBoss = GetComponent<Boss>();
  }

  private void Update()
  {
    // Clean up destroyed mini bosses from list
    activeMiniBosses.RemoveAll(mb => mb == null);

    // Check if we should spawn a new mini boss
    if (ShouldSpawnMiniBoss())
    {
      SpawnMiniBoss();
    }
  }

  private bool ShouldSpawnMiniBoss()
  {
    // Don't spawn if we have max mini bosses
    if (activeMiniBosses.Count >= maxMiniBosses) return false;

    // Don't spawn if main boss is dead
    if (mainBoss == null || mainBoss.CurrentHealth <= 0) return false;

    // Check spawn interval based on current phase
    float requiredInterval = GetCurrentSpawnInterval();
    return Time.time - lastSpawnTime >= requiredInterval;
  }

  private float GetCurrentSpawnInterval()
  {
    if (mainBoss == null) return phase1SpawnInterval;

    // Determine phase based on health percentage
    float healthPercent = mainBoss.HealthPercentage;

    if (healthPercent > 0.5f)
    {
      return phase1SpawnInterval; // Phase 1: Guard Protocol
    }
    else if (healthPercent > 0.1f)
    {
      return phase2SpawnInterval; // Phase 2: Chaotic Overload
    }
    else
    {
      return phase3SpawnInterval; // Phase 3: Core Awakening
    }
  }

  public void SpawnMiniBoss()
  {
    if (miniBossPrefab == null)
    {
      Debug.LogWarning("Mini boss prefab not assigned!");
      return;
    }

    // Find a spawn position around the main boss
    Vector3 spawnPosition = GetSpawnPosition();

    // Spawn the mini boss
    GameObject newMiniBoss = Instantiate(miniBossPrefab, spawnPosition, Quaternion.identity);
    activeMiniBosses.Add(newMiniBoss);

    // Set orbit angle to spread them out evenly
    MiniBoss miniBossScript = newMiniBoss.GetComponent<MiniBoss>();
    if (miniBossScript != null)
    {
      // Distribute mini bosses evenly around the orbit
      float angleOffset = (360f / maxMiniBosses) * (activeMiniBosses.Count - 1);
      miniBossScript.SetOrbitAngle(angleOffset);
      Debug.Log($"Mini boss spawned at orbit angle: {angleOffset}°");
    }

    lastSpawnTime = Time.time;

    Debug.Log($"Spawned mini boss! Active count: {activeMiniBosses.Count}");
  }

  private Vector3 GetSpawnPosition()
  {
    // Spawn at a random position around the main boss
    float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
    Vector3 offset = new Vector3(
        Mathf.Cos(randomAngle) * spawnRadius,
        Mathf.Sin(randomAngle) * spawnRadius,
        0
    );

    return transform.position + offset;
  }

  public void ForceSpawnMiniBoss()
  {
    if (activeMiniBosses.Count < maxMiniBosses)
    {
      SpawnMiniBoss();
    }
  }

  public void DestroyAllMiniBosses()
  {
    foreach (GameObject miniBoss in activeMiniBosses)
    {
      if (miniBoss != null)
      {
        Destroy(miniBoss);
      }
    }
    activeMiniBosses.Clear();
    Debug.Log("All mini bosses destroyed");
  }

  public void SetMaxMiniBosses(int newMax)
  {
    maxMiniBosses = newMax;

    // If we now have too many, destroy the excess
    while (activeMiniBosses.Count > maxMiniBosses)
    {
      GameObject excessMiniBoss = activeMiniBosses[activeMiniBosses.Count - 1];
      activeMiniBosses.RemoveAt(activeMiniBosses.Count - 1);
      if (excessMiniBoss != null)
      {
        Destroy(excessMiniBoss);
      }
    }
  }

  private void OnDrawGizmosSelected()
  {
    // Draw spawn radius
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, spawnRadius);

    // Draw mini boss positions
    Gizmos.color = Color.red;
    foreach (GameObject miniBoss in activeMiniBosses)
    {
      if (miniBoss != null)
      {
        Gizmos.DrawWireSphere(miniBoss.transform.position, 0.5f);
      }
    }
  }
}
