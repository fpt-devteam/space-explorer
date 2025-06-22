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
      Projectile projectile = bullet.GetComponent<Projectile>();
      projectile.direction = (firePoint.position - transform.position).normalized;
      bullet.tag = "PlayerBullet";
    }
  }
  public void SpecialShoot()
  {
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
  }
}
