using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FireAsteroid : MonoBehaviour
{
  [SerializeField] private float minSpeed = 1f;
  [SerializeField] private float maxSpeed = 5f;
  [SerializeField] private GameObject flameEffect;
  private Rigidbody2D rb;
  private Player player;
  private Animator animator;

  private void Awake()
  {
    if (flameEffect == null)
    {
      flameEffect = transform.Find("Fire")?.gameObject;
    }
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
    if (!collision.CompareTag("Bullet")) return;

    Destroy(collision.gameObject);
    ScoreManager.Instance?.AddPoints(2);

    if (flameEffect != null)
      flameEffect.SetActive(false);

    if (animator != null)
      animator.Play("Explode");

    if (rb != null)
      rb.linearVelocity = Vector2.zero;

    StartCoroutine(DestroyAfterAnimation());
  }

  private IEnumerator DestroyAfterAnimation()
  {
    yield return new WaitForSeconds(0.8f);
    Destroy(gameObject);
  }
}
