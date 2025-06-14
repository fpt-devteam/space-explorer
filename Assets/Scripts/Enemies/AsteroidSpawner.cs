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

        int randomPoint = Random.Range(1, 3);

        if (randomPoint == 1)
        {
            if (starPrefab == null || pointA == null) return;

            Vector2 spawnPos1 = RandomPointInTriangle(pointA.position, pointB.position, pointC.position);
            GameObject star = Instantiate(starPrefab, spawnPos1, Quaternion.identity);

            Vector2 direction1 = ((Vector2)pointA.position - spawnPos1).normalized;

            Rigidbody2D rb1 = star.GetComponent<Rigidbody2D>();

            if (rb1 != null)
            {
                float speed = Random.Range(1f, 3f);
                rb1.linearVelocity = direction1 * speed;
            }


            return;
        }
        if (asteroidPrefab == null || pointA == null) return;

        Vector2 spawnPos = RandomPointInTriangle(pointA.position, pointB.position, pointC.position);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        Vector2 direction = ((Vector2)pointA.position - spawnPos).normalized;

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

    private Vector2 RandomPointInTriangle(Vector2 A, Vector2 B, Vector2 C)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        Vector2 point = A + r1 * (B - A) + r2 * (C - A);
        return point;
    }

}
