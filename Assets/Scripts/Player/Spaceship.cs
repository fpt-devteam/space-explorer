using System.Collections;
using UnityEngine;
public class Spaceship : MonoBehaviour
{
  [SerializeField] private GameObject bulletPrefab;
  [SerializeField] private Transform firePoint;
  [SerializeField] private float bulletSpeed = 2f;

  void Awake()
  {
  }

  public void Shoot()
  {
    Debug.Log(GameManager.Instance.CurrentState);
    if (GameManager.Instance.CurrentState == GameState.GameOver) return;

    if (bulletPrefab && firePoint)
    {
      GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(Vector3.forward, firePoint.position - transform.position));
      bullet.tag = "PlayerBullet";

      Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
      bulletRb.linearVelocity = (firePoint.position - transform.position).normalized * bulletSpeed;
    }
  }
  public void SpecialShoot()
  {
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Asteroid"))
    {
      // Handle collision with asteroid
      // For example, you can reduce health or destroy the asteroid
      Destroy(collision.gameObject);
      // health.UseHealth(1);
    }
    else if (collision.gameObject.CompareTag("Enemy"))
    {
      // Handle collision with enemy
      // For example, you can reduce health or destroy the enemy
      Destroy(collision.gameObject);
      // health.UseHealth(1);
    }
    else if (collision.gameObject.CompareTag("EnemyBullet"))
    {
      // health.UseHealth(1);
      Destroy(collision.gameObject);
    }
  }
}
