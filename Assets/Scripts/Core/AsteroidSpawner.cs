using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject fireAsteroidPrefab;
    [SerializeField] private float spawnInterval = 0f;
    [SerializeField] private Transform player;

    private bool spawning = true;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

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
        if (asteroidPrefab == null)
        {
            Debug.LogWarning("Asteroid prefab is not assigned.");
            return;
        }

        if (fireAsteroidPrefab == null)
        {
            Debug.LogWarning("Fire Asteroid prefab is not assigned.");
            return;
        }

        Vector2 spawnPos = RandomPointInCircleAroundPlayer(10f);

        if (Random.Range(0f, 1f) < 0.5f)
        {
            Instantiate(fireAsteroidPrefab, spawnPos, Quaternion.identity);
            return;
        }

        Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        spawning = false;
    }

    private Vector2 RandomPointInCircleAroundPlayer(float radius)
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float r = Random.Range(0.5f * radius, radius);
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * r;
        return (Vector2)player.position + offset;
    }
}
