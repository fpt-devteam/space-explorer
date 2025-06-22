using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns asteroids at random positions and intervals.
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;

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

        Vector2 spawnPos = RandomPointInSemicircle(5f);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Vector2 direction = ((Vector2)transform.position - spawnPos).normalized;

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float speed = Random.Range(1f, 3f);
            rb.linearVelocity = direction * speed;
        }
    }
    public void StopSpawning()
    {
        spawning = false;
    }
    private Vector2 RandomPointInSemicircle(float radius)
    {
        float angle = Random.Range(-90f, 90f) * Mathf.Deg2Rad; // nửa vòng trên
        float r = Random.Range(0.5f * radius, radius); // để không quá gần giữa

        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * r;
        return (Vector2)transform.position + offset;
    }


}
