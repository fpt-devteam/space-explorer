using UnityEngine;

/// <summary>
/// Handles laser projectile logic.
/// </summary>
public class Laser : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
