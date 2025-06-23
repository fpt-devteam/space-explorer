using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed = 2f;
  [SerializeField] private float lifetime = 20f;
  [SerializeField] private float shootCooldown = 5;

  public Vector3 direction { get; set; }

  private void Start()
  {
    Destroy(gameObject, lifetime);
  }

  private void Update()
  {
    transform.position += direction * speed * Time.deltaTime;
  }
}
