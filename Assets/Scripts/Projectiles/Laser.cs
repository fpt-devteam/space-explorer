using UnityEngine;

/// <summary>
/// Handles laser projectile logic.
/// </summary>
public class Laser : MonoBehaviour
{
  [SerializeField] private float lifeTime = 2f;
  [SerializeField] private float speed = 10f;

  public Vector3 direction { get; set; }

  private void Start()
  {
    Destroy(gameObject, lifeTime);
  }

  private void Update()
  {
    transform.position += direction * speed * Time.deltaTime;
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
