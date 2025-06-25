using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Asteroid : MonoBehaviour
{
  [SerializeField] private float minSpeed = 1f;
  [SerializeField] private float maxSpeed = 5f;
  private Rigidbody2D rb;
  private Player player;
  private Animator animator;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    animator = GetComponent<Animator>();
  }

  private void Start()
  {
    if (player)
    {
      rb = GetComponent<Rigidbody2D>();
      float speed = Random.Range(minSpeed, maxSpeed);

      Vector3 direction = player.transform.position - transform.position;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, angle + 180f);

      Vector2 movementDir = (player.transform.position - transform.position).normalized;
      rb.linearVelocity = movementDir * speed;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player") || collision.CompareTag("PlayerBullet"))
    {
      SoundManager.Instance.PlaySFX(SoundManager.Instance.boomAsteroid);
      animator.Play("Explode");
      rb.linearVelocity = Vector2.zero;
      StartCoroutine(DestroyAfterAnimation());
    }
  }

  private IEnumerator DestroyAfterAnimation()
  {
    yield return new WaitForSeconds(0.8f);
    Destroy(gameObject);
  }
}
