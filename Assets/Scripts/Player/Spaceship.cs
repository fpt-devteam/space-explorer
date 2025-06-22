using System.Collections;
using UnityEngine;
public class Spaceship : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private GameOverUI gameOverUI;


    void Awake()
    {
        GameObject uiObject = GameObject.FindWithTag("GameManager");
        if (uiObject != null)
        {
            gameOverUI = uiObject.GetComponent<GameOverUI>();
        }

    }
    public void Shoot()
    {
        if (GameManager.Instance.CurrentState == GameState.GameOver) return;
        if (!bulletPrefab)
            Debug.LogError("bulletPrefab is null!");

        if (!firePoint)
            Debug.LogError("firePoint is null!");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            print("Spceship Hit by asteroid!");
            GameManager.Instance.EndGame();
            //Get GameOverUI and show it

            gameOverUI.ShowGameOver();
        }
        else if (collision.CompareTag("Star"))
        {
            ScoreManager.Instance.AddPoints(1);
            Destroy(collision.gameObject);
        }
    }
}
