using UnityEngine;

/// <summary>
/// Phase 2: Chaotic Overload State
/// Boss continues staying in center, fires continuous laser that slightly rotates following player for 3 seconds
/// Attack pattern: Track 3s → rest 1s → repeat
/// Summons "Omega Formation" enemies and more aggressive mini bosses
/// Transitions when health drops below 10%
/// </summary>
public class ChaoticOverloadState : BaseState<Boss>
{
  private enum SubState { Tracking, Resting }

  private SubState currentSubState = SubState.Resting;
  private float subStateTimer;
  private float trackingDuration = 3f;
  private float restDuration = 1f;
  private float rotationSpeed = 30f;
  private bool hasIncreasedMiniBosses = false;

  protected override void OnEnter(Boss boss)
  {
    Debug.Log("Boss entering Chaotic Overload State");
    currentSubState = SubState.Resting;
    subStateTimer = 0f;
    hasIncreasedMiniBosses = false;

    // Increase mini boss limit for this phase (higher difficulty)
    boss.SetMiniBossLimit(3);

    SpawnOmegaFormation(boss);
  }

  protected override void OnUpdate(Boss boss)
  {
    subStateTimer += Time.deltaTime;

    // Spawn additional mini boss after entering this phase
    if (!hasIncreasedMiniBosses && StateTime > 1f)
    {
      if (boss.GetActiveMiniBossCount() < 3)
      {
        boss.SpawnMiniBoss();
        Debug.Log("Chaotic Overload: Additional mini boss spawned");
      }
      hasIncreasedMiniBosses = true;
    }

    switch (currentSubState)
    {
      case SubState.Resting:
        HandleRestingState(boss);
        break;

      case SubState.Tracking:
        HandleTrackingState(boss);
        break;
    }
  }

  protected override void OnExit(Boss boss)
  {
    Debug.Log("Boss exiting Chaotic Overload State");
    Debug.Log($"Phase transition: {boss.GetActiveMiniBossCount()} mini bosses remain active");
  }

  protected override IState<Boss> OnCheckTransitions(Boss boss)
  {
    if (boss.HealthPercentage <= 0.1f)
    {
      return boss.GetCoreAwakeningState();
    }

    return null;
  }

  private void HandleRestingState(Boss boss)
  {
    if (subStateTimer >= restDuration)
    {
      currentSubState = SubState.Tracking;
      subStateTimer = 0f;
      Debug.Log("Boss starting laser tracking phase");
    }
  }

  private void HandleTrackingState(Boss boss)
  {
    Vector3 playerDirection = boss.GetPlayerDirection();

    float targetAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
    float currentAngle = boss.transform.eulerAngles.z;

    float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
    boss.transform.rotation = Quaternion.Euler(0, 0, newAngle);

    boss.FireLaser(boss.GetPlayerDirection());

    if (subStateTimer >= trackingDuration)
    {
      currentSubState = SubState.Resting;
      subStateTimer = 0f;
      Debug.Log("Boss entering rest phase");
    }
  }

  private void SpawnOmegaFormation(Boss boss)
  {
    Debug.Log("Spawning Omega Formation enemies");
    // Implementation for spawning Omega Formation enemies
    // This would be more aggressive than Alpha formation
    // For example:
    // EnemyFormationSpawner.Instance?.SpawnFormation(FormationType.Omega);
  }
}
