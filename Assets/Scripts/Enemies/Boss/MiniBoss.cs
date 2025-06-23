using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Mini boss that spawns during boss phases
/// Orbits around the main boss and attacks the player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MiniBoss : MonoBehaviour
{
  [Header("Mini Boss Stats")]
  [SerializeField] private float maxHealth = 30f;
  [SerializeField] private float currentHealth;
  [SerializeField] private float damage = 15f;

  [Header("Movement Settings")]
  [SerializeField] private float orbitSpeed = 60f; // degrees per second
  [SerializeField] private float orbitRadius = 3f;
  [SerializeField] private float targetSpeed = 2f;
  [SerializeField] private float returnSpeed = 1.5f;

  [Header("Attack Settings")]
  [SerializeField] private float attackCooldown = 3f;
  [SerializeField] private float attackRange = 6f;
  [SerializeField] private GameObject projectilePrefab;
  [SerializeField] private Transform firePoint;

  [Header("UI Components")]
  [SerializeField] private Slider healthSlider;

  private Transform mainBoss;
  private GameObject player;
  private Rigidbody2D rb;
  private MiniBossStateMachine stateMachine;

  // State tracking
  private float orbitAngle;
  private float lastAttackTime;
  private Vector3 orbitCenter;

  public float MaxHealth => maxHealth;
  public float CurrentHealth => currentHealth;
  public float HealthPercentage => currentHealth / maxHealth;
  public Transform MainBoss => mainBoss;
  public GameObject Player => player;
  public float OrbitRadius => orbitRadius;
  public float TargetSpeed => targetSpeed;
  public float ReturnSpeed => returnSpeed;
  public float AttackRange => attackRange;
  public float OrbitAngle => orbitAngle;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    currentHealth = maxHealth;

    // Find references
    player = GameObject.FindGameObjectWithTag("Player");

    // Initialize state machine
    stateMachine = new MiniBossStateMachine(this);

    // Initialize health slider
    UpdateHealthSlider();
  }

  private void Start()
  {
    // Find main boss and set orbit center
    Boss boss = Object.FindObjectOfType<Boss>();
    if (boss != null)
    {
      mainBoss = boss.transform;
      orbitCenter = mainBoss.position;
    }

    // Start orbiting
    stateMachine.ChangeState(MiniBossState.Orbiting);
  }

  private void Update()
  {
    stateMachine.Update();

    // Update orbit center to follow main boss
    if (mainBoss != null)
    {
      orbitCenter = mainBoss.position;
    }

    // Keep health slider facing camera if it exists
    UpdateHealthSliderOrientation();
  }

  public void TakeDamage(float damageAmount)
  {
    currentHealth = Mathf.Max(0, currentHealth - damageAmount);

    // Update health slider
    UpdateHealthSlider();

    if (currentHealth <= 0)
    {
      Die();
    }
  }

  private void UpdateHealthSlider()
  {
    if (healthSlider != null)
    {
      healthSlider.value = HealthPercentage;

      // Optional: Change slider color based on health
      Image fillImage = healthSlider.fillRect.GetComponent<Image>();
      if (fillImage != null)
      {
        if (HealthPercentage > 0.6f)
          fillImage.color = Color.green;
        else if (HealthPercentage > 0.3f)
          fillImage.color = Color.yellow;
        else
          fillImage.color = Color.red;
      }
    }
  }

  private void UpdateHealthSliderOrientation()
  {
    // Keep health slider facing the camera
    if (healthSlider != null)
    {
      Camera mainCamera = Camera.main;
      if (mainCamera != null)
      {
        healthSlider.transform.LookAt(mainCamera.transform);
        healthSlider.transform.Rotate(0, 180, 0); // Flip it to face the camera properly
      }
    }
  }

  private void Die()
  {
    Debug.Log("Mini boss defeated!");

    // Hide health slider when mini boss dies
    if (healthSlider != null)
    {
      healthSlider.gameObject.SetActive(false);
    }

    // Add score
    if (ScoreManager.Instance != null)
    {
      ScoreManager.Instance.AddPoints(5); // More points than regular enemies
    }

    // Spawn death effect if needed
    Destroy(gameObject);
  }

  public void OrbitAroundBoss()
  {
    if (mainBoss == null) return;

    // Update orbit angle
    orbitAngle += orbitSpeed * Time.deltaTime;
    if (orbitAngle >= 360f) orbitAngle -= 360f;

    // Calculate orbit position
    float radians = orbitAngle * Mathf.Deg2Rad;
    Vector3 orbitPosition = orbitCenter + new Vector3(
        Mathf.Cos(radians) * orbitRadius,
        Mathf.Sin(radians) * orbitRadius,
        0
    );

    // Move towards orbit position
    Vector3 direction = (orbitPosition - transform.position).normalized;
    rb.linearVelocity = direction * returnSpeed;

    // Rotate to face movement direction
    if (direction != Vector3.zero)
    {
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }
  }

  public void MoveTowardsPlayer()
  {
    if (player == null) return;

    Vector3 direction = (player.transform.position - transform.position).normalized;
    rb.linearVelocity = direction * targetSpeed;

    // Rotate to face player
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.Euler(0, 0, angle);
  }

  public void ReturnToOrbit()
  {
    if (mainBoss == null) return;

    // Calculate target orbit position
    float radians = orbitAngle * Mathf.Deg2Rad;
    Vector3 targetPosition = orbitCenter + new Vector3(
        Mathf.Cos(radians) * orbitRadius,
        Mathf.Sin(radians) * orbitRadius,
        0
    );

    Vector3 direction = (targetPosition - transform.position).normalized;
    rb.linearVelocity = direction * returnSpeed;

    // Rotate to face movement direction
    if (direction != Vector3.zero)
    {
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }
  }

  public void AttackPlayer()
  {
    if (player == null || projectilePrefab == null || firePoint == null) return;
    if (Time.time - lastAttackTime < attackCooldown) return;

    Vector3 direction = (player.transform.position - firePoint.position).normalized;

    GameObject projectile = Instantiate(projectilePrefab, firePoint.position,
    Quaternion.LookRotation(Vector3.forward, direction));

    // Set projectile properties
    Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
    if (projectileRb != null)
    {
      projectileRb.linearVelocity = direction * 8f; // Projectile speed
    }

    // Change tag to enemy projectile
    projectile.tag = "EnemyBullet";

    lastAttackTime = Time.time;
    Debug.Log("Mini boss attacking player!");
  }

  public float GetDistanceToPlayer()
  {
    if (player == null) return float.MaxValue;
    return Vector3.Distance(transform.position, player.transform.position);
  }

  public bool IsInAttackRange()
  {
    return GetDistanceToPlayer() <= attackRange;
  }

  public bool IsNearOrbitPosition()
  {
    if (mainBoss == null) return false;

    float radians = orbitAngle * Mathf.Deg2Rad;
    Vector3 targetPosition = orbitCenter + new Vector3(
        Mathf.Cos(radians) * orbitRadius,
        Mathf.Sin(radians) * orbitRadius,
        0
    );

    return Vector3.Distance(transform.position, targetPosition) < 0.5f;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("PlayerBullet"))
    {
      TakeDamage(50f);
      Destroy(collision.gameObject);
    }
    else if (collision.CompareTag("Player"))
    {
      Player playerScript = collision.GetComponent<Player>();
      if (playerScript != null)
      {
        if (playerScript.currentShield > 0f)
        {
          playerScript.currentShield -= 1;
        }
        else
        {
          playerScript.currentHealth = 0;
        }
      }

      Die();
    }
  }

  /// <summary>
  /// Set the starting orbit angle for this mini boss
  /// </summary>
  public void SetOrbitAngle(float angle)
  {
    orbitAngle = angle;
  }
}
