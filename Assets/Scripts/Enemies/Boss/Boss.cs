using UnityEngine;

/// <summary>
/// Example implementation of the generic state machine for the Boss
/// </summary>
public class Boss : StateMachineController<Boss>
{
  [Header("Boss Stats")]
  [SerializeField] private float maxHealth = 100f;
  [SerializeField] private float currentHealth;

  [Header("Boss Components")]
  [SerializeField] private Transform firePoint;
  [SerializeField] private GameObject laserPrefab;

  // Boss states - these will be created as separate classes
  private GuardProtocolState guardProtocolState;
  private ChaoticOverloadState chaoticOverloadState;
  private CoreAwakeningState coreAwakeningState;

  // Mini boss spawner
  private MiniBossSpawner miniBossSpawner;

  public float MaxHealth => maxHealth;
  public float CurrentHealth => currentHealth;
  public float HealthPercentage => currentHealth / maxHealth;
  public Transform FirePoint => firePoint;
  public GameObject LaserPrefab => laserPrefab;
  public MiniBossSpawner MiniBossSpawner => miniBossSpawner;

  protected override void Awake()
  {
    base.Awake();
    currentHealth = maxHealth;

    guardProtocolState = new GuardProtocolState();
    chaoticOverloadState = new ChaoticOverloadState();
    coreAwakeningState = new CoreAwakeningState();

    // Get or add mini boss spawner component
    miniBossSpawner = GetComponent<MiniBossSpawner>();
    if (miniBossSpawner == null)
    {
      miniBossSpawner = gameObject.AddComponent<MiniBossSpawner>();
    }

    print($"Boss spawned with {miniBossSpawner.ActiveMiniBossCount} mini bosses in position {transform.position}");
  }

  protected override void InitializeStateMachine()
  {
    stateMachine.Initialize(this, guardProtocolState);
  }

  public void TakeDamage(float damage)
  {
    currentHealth = Mathf.Max(0, currentHealth - damage);

    if (currentHealth <= 0)
    {
      Die();
    }
  }

  private void Die()
  {
    Debug.Log("Boss defeated!");

    // Destroy all mini bosses when main boss dies
    if (miniBossSpawner != null)
    {
      miniBossSpawner.DestroyAllMiniBosses();
    }

    gameObject.SetActive(false);
  }

  public void FireLaser(Vector3 direction)
  {
    if (laserPrefab && firePoint)
    {
      GameObject bullet = Instantiate(laserPrefab, firePoint.position, Quaternion.LookRotation(Vector3.forward, direction));
      Projectile projectile = bullet.GetComponent<Projectile>();
      projectile.direction = GetPlayerDirection();
    }
  }

  public Vector3 GetPlayerDirection()
  {
    GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
      return (player.transform.position - transform.position).normalized;
    }
    return Vector3.up;
  }

  // Mini boss management methods
  public void SpawnMiniBoss()
  {
    if (miniBossSpawner != null)
    {
      miniBossSpawner.ForceSpawnMiniBoss();
    }
  }

  public int GetActiveMiniBossCount()
  {
    return miniBossSpawner != null ? miniBossSpawner.ActiveMiniBossCount : 0;
  }

  public void SetMiniBossLimit(int limit)
  {
    if (miniBossSpawner != null)
    {
      miniBossSpawner.SetMaxMiniBosses(limit);
    }
  }

  public GuardProtocolState GetGuardProtocolState() => guardProtocolState;
  public ChaoticOverloadState GetChaoticOverloadState() => chaoticOverloadState;
  public CoreAwakeningState GetCoreAwakeningState() => coreAwakeningState;

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("PlayerBullet"))
    {
      TakeDamage(10);
    }
  }
}
