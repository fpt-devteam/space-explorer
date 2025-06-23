using System.Collections;
using UnityEngine;

public enum SkillType
{
  DefaultShoot,
  SpecialShoot
}

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
  private Player player;
  private Spaceship spaceship;
  private Rigidbody2D rb;
  private bool isShooting;
  private Animator animator;
  private bool isInvincible = false;
  private float invincibleTimer = 0f;
  [SerializeField] private float invincibleDuration = 3f;
  [SerializeField] private float shootInterval = 0.9f;

  public void Restart()
  {
    isInvincible = false;
    invincibleTimer = 0f;
    isShooting = true;
    player.InitStats();
  }

  private void Awake()
  {
    player = GetComponent<Player>();
    rb = GetComponent<Rigidbody2D>();
    spaceship = GetComponent<Spaceship>();
    animator = player.GetComponent<Animator>();
  }
  private void Start()
  {
    isShooting = true;
    StartCoroutine(ShootCoroutine());
  }
  private void Update()
  {
    HandleMovement();
    HandleSkills();
  }
  private void HandleSkills()
  {
    var spriteRenderer = spaceship.GetComponent<SpriteRenderer>();

    if (isInvincible)
    {
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer <= 0f)
      {
        Debug.Log("Invincibility ended.");
        isInvincible = false;
        spriteRenderer.color = Color.white;
      }
      else
      {
        float t = Mathf.PingPong(Time.time * 5f, 1f);
        spriteRenderer.color = Color.Lerp(Color.black, Color.yellow, t);
      }
    }
  }
  private IEnumerator ShootCoroutine()
  {
    while (isShooting && spaceship != null)
    {
      spaceship.Shoot();
      yield return new WaitForSeconds(shootInterval);
    }
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    Debug.Log("Player collided with: " + collision.gameObject.name);

    if (collision.CompareTag("Asteroid") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
    {
      if (player.currentShield > 0f)
      {
        player.currentShield -= 1;
        return;
      }

      if (isInvincible)
      {
        Debug.Log("Player is invincible, no damage taken.");
        return;
      }

      Debug.Log("Player took damage from asteroid.");
      player.currentHealth -= 1;

      if (player.currentHealth <= 0f)
      {
        animator.Play("Destruction");
        GameManager.Instance.EndGame();
        StartCoroutine(ShowGameOverAfterAnimation());
      }
      else
      {
        isInvincible = true;
        invincibleTimer = invincibleDuration;
        Debug.Log("Player is invincible for 3 seconds.");
      }
    }
  }
  private IEnumerator ShowGameOverAfterAnimation()
  {
    yield return new WaitForSeconds(2f);
    CanvasManager.Instance.ShowGameOverMenu();
  }
  private void HandleMovement()
  {
    float moveX = Input.GetAxis("Horizontal");
    float moveY = Input.GetAxis("Vertical");

    Vector2 moveDir = new Vector2(moveX, moveY).normalized;
    rb.linearVelocity = moveDir * player.MoveSpeed;

    float baseRotationSpeed = 180f;
    if (Input.GetMouseButton(0))
    {
      transform.Rotate(0f, 0f, baseRotationSpeed * Time.deltaTime);
    }
    if (Input.GetMouseButton(1))
    {
      transform.Rotate(0f, 0f, -baseRotationSpeed * Time.deltaTime);
    }
  }
}
