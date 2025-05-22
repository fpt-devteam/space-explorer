using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns asteroids at random positions and intervals.
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 10f;
    private bool spawning = true;

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (spawning)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnAsteroid()
    {
        if (asteroidPrefab == null) return;
        Vector2 spawnPos = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
