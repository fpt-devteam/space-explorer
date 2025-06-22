using System.Collections;
using UnityEngine;
public class Spaceship : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 2f;
    private HealthSystem health;
    private StaminaSystem stamina;
    private ShieldSystem shield;

    void Awake()
    {
        health = GetComponent<HealthSystem>();
        stamina = GetComponent<StaminaSystem>();
        shield = GetComponent<ShieldSystem>();
    }
    public void Shoot()
    {
        if (GameManager.Instance.CurrentState == GameState.GameOver) return;

        if (bulletPrefab && firePoint)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb)
            {
                rb.linearVelocity = firePoint.up * bulletSpeed;
            }
        }
    }
    public void SpecialShoot()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Asteroid"))
        // {
        //     print("Spaceship Hit by asteroid!");
        //     // GameManager.Instance.EndGame();
        //     //Get GameOverUI and show it

        //     // gameOverUI.ShowGameOver();
        // }
        // else if (collision.CompareTag("Star"))
        // {
        //     ScoreManager.Instance.AddPoints(1);
        //     Destroy(collision.gameObject);
        // }
    }
}
