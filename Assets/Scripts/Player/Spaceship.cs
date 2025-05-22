using UnityEngine;

/// <summary>
/// Handles spaceship shooting and collision logic.
/// </summary>
public class Spaceship : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float laserSpeed = 10f;

    /// <summary>
    /// Instantiates a laser and sets its velocity.
    /// </summary>
    public void Shoot()
    {
        if (laserPrefab && firePoint)
        {
            GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            if (rb)
                rb.linearVelocity = firePoint.up * laserSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            GameManager.Instance.EndGame();
        }
        else if (collision.CompareTag("Star"))
        {
            ScoreManager.Instance.AddPoints(1);
            Destroy(collision.gameObject);
        }
    }
}
