using UnityEngine;

/// <summary>
/// Handles asteroid movement and collision.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 3f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float speed = Random.Range(minSpeed, maxSpeed);
        rb.linearVelocity = randomDir * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
