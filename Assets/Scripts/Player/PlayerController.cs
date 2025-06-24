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
  [SerializeField] private CanvasManager canvasManager;

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
    if (isInvincible)
    {
      invincibleTimer += Time.deltaTime;
      if (invincibleTimer >= invincibleDuration)
      {
        isInvincible = false;
        invincibleTimer = 0f;
        GetComponent<SpriteRenderer>().color = Color.white;
      }
      else
      {
        float t = Mathf.PingPong(Time.time * 5f, 1f);
        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.yellow, t);
      }
    }

    if (player.currentHealth <= 0f)
    {
      OnExplode();
      return;
    }

    canvasManager.UpdateHealthBar(player.currentHealth);
    canvasManager.UpdateShieldBar(player.currentShield);

    HandleMovement();
    HandleSkills();
  }
  private void HandleSkills()
  {

  }
  private IEnumerator ShootCoroutine()
  {
    print($"Shooting started. {spaceship != null} {isShooting} {shootInterval}");
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
        OnDeductShield(1);
        OnInvincibility();
      }
      else
      {
        OnDeductHealth(1);

        if (player.currentHealth > 0f)
        {
          OnInvincibility();
        }
        else
        {
          OnExplode();
        }
      }
    }
  }
  private void OnInvincibility()
  {
    if (!isInvincible)
    {
      isInvincible = true;
      invincibleTimer = 0f;
    }
  }
  private void OnDeductHealth(int healthAmount)
  {
    if (isInvincible) return;
    player.currentHealth -= healthAmount;
  }
  private void OnDeductShield(int shieldAmount)
  {
    if (isInvincible) return;
    player.currentShield -= shieldAmount;
  }
  private void OnExplode()
  {
    animator.Play("Destruction");
    SoundManager.Instance.PlaySFX(SoundManager.Instance.boomSpaceShip);
    GameManager.Instance.EndGame();
    StartCoroutine(ShowGameOverAfterAnimation());
  }
  private IEnumerator ShowGameOverAfterAnimation()
  {
    yield return new WaitForSeconds(2f);
    canvasManager.ShowGameOverMenu();
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
