using UnityEngine;

/// <summary>
/// Phase 1: Guard Protocol State
/// Boss stands fixed at center, charges energy and fires laser aimed at player's current position
/// Summons "Alpha Formation" enemies
/// Spawns mini bosses that orbit around the boss
/// Transitions when health drops below 50%
/// </summary>
public class GuardProtocolState : BaseState<Boss>
{
  private float lastLaserTime;
  private float laserCooldown = 2f;
  private float chargeTime = 1f;
  private bool isCharging;
  private float chargeStartTime;
  private bool hasSpawnedInitialMiniBoss = false;

  protected override void OnEnter(Boss boss)
  {
    Debug.Log("Boss entering Guard Protocol State");
    lastLaserTime = 0f;
    isCharging = false;
    hasSpawnedInitialMiniBoss = false;

    // Set mini boss limit for this phase (moderate difficulty)
    boss.SetMiniBossLimit(2);

    // Spawn Alpha Formation enemies
    SpawnAlphaFormation(boss);
  }

  protected override void OnUpdate(Boss boss)
  {
    // Spawn initial mini boss after a short delay
    if (!hasSpawnedInitialMiniBoss && StateTime > 2f)
    {
      boss.SpawnMiniBoss();
      hasSpawnedInitialMiniBoss = true;
      Debug.Log("Guard Protocol: Initial mini boss spawned");
    }

    // Handle laser charging and firing
    if (!isCharging && StateTime - lastLaserTime >= laserCooldown)
    {
      StartCharging(boss);
    }

    if (isCharging && StateTime - chargeStartTime >= chargeTime)
    {
      FireLaser(boss);
    }
  }

  protected override void OnExit(Boss boss)
  {
    Debug.Log("Boss exiting Guard Protocol State");
    isCharging = false;

    // Keep mini bosses alive for next phase - they'll continue orbiting
    Debug.Log($"Phase transition: {boss.GetActiveMiniBossCount()} mini bosses remain active");
  }

  protected override IState<Boss> OnCheckTransitions(Boss boss)
  {
    // Transition to Chaotic Overload when health drops below 50%
    if (boss.HealthPercentage <= 0.5f)
    {
      return boss.GetChaoticOverloadState();
    }

    return null;
  }

  private void StartCharging(Boss boss)
  {
    isCharging = true;
    chargeStartTime = StateTime;
    Debug.Log("Boss charging laser...");
  }

  private void FireLaser(Boss boss)
  {
    Vector3 playerDirection = boss.GetPlayerDirection();
    boss.FireLaser(playerDirection);

    Debug.Log("Boss fired laser at player!");

    // Reset timing
    lastLaserTime = StateTime;
    isCharging = false;
  }

  private void SpawnAlphaFormation(Boss boss)
  {
    Debug.Log("Spawning Alpha Formation enemies");
    // Implementation for spawning Alpha Formation enemies
    // This would typically use your enemy spawning system
    // For example:
    // EnemyFormationSpawner.Instance?.SpawnFormation(FormationType.Alpha);
  }
}
