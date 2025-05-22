using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns stars at random positions and intervals.
/// </summary>
public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Vector2 areaSize = new Vector2(15f, 8f);
    private bool spawning = true;

    private void Start()
    {
        StartCoroutine(SpawnStars());
    }

    private IEnumerator SpawnStars()
    {
        while (spawning)
        {
            SpawnStar();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnStar()
    {
        if (starPrefab == null) return;
        Vector2 spawnPos = new Vector2(
            Random.Range(-areaSize.x / 2, areaSize.x / 2),
            Random.Range(-areaSize.y / 2, areaSize.y / 2)
        );
        Instantiate(starPrefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        spawning = false;
    }
}
