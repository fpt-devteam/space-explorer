using UnityEngine;

/// <summary>
/// Phase 3: Core Awakening State
/// Boss activates projectile reflection shield (all bullets that touch are reflected back)
/// Shield only turns off when boss is firing rotating laser
/// Boss stays still as center for circular attack
/// Summons "Sigma Formation" enemies and maximum mini bosses
/// Final Attack: When health at 1% or all enemies defeated - 360° laser sweep across entire map
/// </summary>
public class CoreAwakeningState : BaseState<Boss>
{
  private enum SubState { Shielded, LaserAttack, FinalAttack }

  private SubState currentSubState = SubState.Shielded;
  private float subStateTimer;
  private float shieldDuration = 5f;
  private float laserAttackDuration = 3f;
  private bool hasShield = true;
  private float rotationAngle = 0f;
  private float rotationSpeed = 90f;
  private bool hasMaximizedMiniBosses = false;

  private ProjectileReflector reflector;

  protected override void OnEnter(Boss boss)
  {
    Debug.Log("Boss entering Core Awakening State - Final Phase!");
    currentSubState = SubState.Shielded;
    subStateTimer = 0f;
    hasShield = true;
    rotationAngle = 0f;
    hasMaximizedMiniBosses = false;

    // Maximize mini boss limit for final phase (maximum difficulty)
    boss.SetMiniBossLimit(4);

    reflector = boss.GetComponent<ProjectileReflector>();
    if (reflector == null)
    {
      reflector = boss.gameObject.AddComponent<ProjectileReflector>();
    }

    ActivateShield(boss);

    SpawnSigmaFormation(boss);
  }

  protected override void OnUpdate(Boss boss)
  {
    subStateTimer += Time.deltaTime;

    // Spawn maximum mini bosses for final phase
    if (!hasMaximizedMiniBosses && StateTime > 1f)
    {
      // Spawn mini bosses up to the limit
      while (boss.GetActiveMiniBossCount() < 4)
      {
        boss.SpawnMiniBoss();
      }
      hasMaximizedMiniBosses = true;
      Debug.Log("Core Awakening: Maximum mini bosses deployed!");
    }

    switch (currentSubState)
    {
      case SubState.Shielded:
        HandleShieldedState(boss);
        break;

      case SubState.LaserAttack:
        HandleLaserAttackState(boss);
        break;

      case SubState.FinalAttack:
        HandleFinalAttackState(boss);
        break;
    }
  }

  protected override void OnExit(Boss boss)
  {
    Debug.Log("Boss exiting Core Awakening State");
    DeactivateShield(boss);
  }

  protected override IState<Boss> OnCheckTransitions(Boss boss)
  {
    if (boss.HealthPercentage <= 0.01f || AreAllEnemiesDefeated())
    {
      if (currentSubState != SubState.FinalAttack)
      {
        currentSubState = SubState.FinalAttack;
        subStateTimer = 0f;
        rotationAngle = 0f;
        DeactivateShield(boss);

        // Destroy all mini bosses during final attack for dramatic effect
        boss.MiniBossSpawner.DestroyAllMiniBosses();
        Debug.Log("FINAL ATTACK INITIATED! All mini bosses recalled!");
      }
    }

    return null;
  }

  private void HandleShieldedState(Boss boss)
  {
    if (subStateTimer >= shieldDuration)
    {
      currentSubState = SubState.LaserAttack;
      subStateTimer = 0f;
      DeactivateShield(boss);
      Debug.Log("Boss deactivating shield for laser attack");
    }
  }

  private void HandleLaserAttackState(Boss boss)
  {
    rotationAngle += rotationSpeed * Time.deltaTime;
    boss.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

    boss.FireLaser(boss.GetPlayerDirection());

    if (subStateTimer >= laserAttackDuration)
    {
      currentSubState = SubState.Shielded;
      subStateTimer = 0f;
      ActivateShield(boss);
      Debug.Log("Boss reactivating shield");
    }
  }

  private void HandleFinalAttackState(Boss boss)
  {
    rotationAngle += rotationSpeed * Time.deltaTime;
    boss.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

    for (int i = 0; i < 8; i++)
    {
      float angle = rotationAngle + (i * 45f);
      Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
      boss.FireLaser(direction);
    }

    Debug.Log("Final attack in progress - 360° laser sweep!");

    if (rotationAngle >= 360f)
    {
      Debug.Log("Final attack complete - Boss defeated!");
      boss.TakeDamage(boss.CurrentHealth);
    }
  }

  private void ActivateShield(Boss boss)
  {
    hasShield = true;
    if (reflector != null)
    {
      reflector.enabled = true;
    }
    Debug.Log("Boss shield activated - projectiles will be reflected!");
  }

  private void DeactivateShield(Boss boss)
  {
    hasShield = false;
    if (reflector != null)
    {
      reflector.enabled = false;
    }
    Debug.Log("Boss shield deactivated");
  }

  private void SpawnSigmaFormation(Boss boss)
  {
    Debug.Log("Spawning Sigma Formation enemies - Final wave!");
    // Implementation for spawning Sigma Formation enemies
    // This would be the most challenging formation
    // For example:
    // EnemyFormationSpawner.Instance?.SpawnFormation(FormationType.Sigma);
  }

  private bool AreAllEnemiesDefeated()
  {
    // Check if all spawned enemies (including mini bosses) are defeated
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Asteroid");

    // Find the boss to get mini boss count
    Boss boss = Object.FindObjectOfType<Boss>();
    int miniBossCount = boss != null ? boss.GetActiveMiniBossCount() : 0;

    int totalEnemies = enemies.Length + miniBossCount;
    return totalEnemies == 0;
  }
}
