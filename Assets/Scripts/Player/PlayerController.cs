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
  public Animator shieldAnimator;

  [SerializeField] private float shootInterval = 0.9f;

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
    if (player.currentShield > 0f)
    {
      shieldAnimator.Play("Battlecruise_Shield");
    }
    else
    {
      shieldAnimator.Play("New State");
    }

    HandleMovement();
    HandleSkills();
  }
  private void HandleSkills()
  {
    // if (Input.GetKeyDown(KeyCode.Space))
    // {
    //     skillSystem.ExecuteSkill(SkillType.SpecialShoot, player);
    // }
    // else
    // {
    //     skillSystem.ExecuteSkill(SkillType.DefaultShoot, player);
    // }

    // skillSystem.ExecuteSkill(SkillType.DefaultShoot, player);
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

    if (collision.CompareTag("Asteroid"))
    {
      if (player.currentShield > 0f)
      {
        player.currentShield -= 1;
      }
      else
      {
        Debug.Log("Player collided with an asteroid and lost health.");
        player.currentHealth -= 1;
        if (player.currentHealth <= 0f)
        {
          animator.Play("Destruction");
          GameManager.Instance.EndGame();
          StartCoroutine(ShowGameOverAfterAnimation());
          Debug.Log("Player has died.");
        }
      }
    }
  }
  private IEnumerator ShowGameOverAfterAnimation()
  {
    yield return new WaitForSeconds(2f); // Wait for the destruction animation to finish
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
