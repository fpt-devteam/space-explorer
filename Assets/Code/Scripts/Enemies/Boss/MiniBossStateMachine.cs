using UnityEngine;

/// <summary>
/// Simple state machine for mini boss behavior
/// </summary>
public enum MiniBossState
{
  Orbiting,    // Orbiting around main boss
  Targeting,   // Moving towards player to attack
  Returning    // Returning to orbit after attack
}

public class MiniBossStateMachine
{
  private MiniBoss miniBoss;
  private MiniBossState currentState;
  private float stateTimer;
  private float targetingDuration = 4f; // Time to target player before returning
  private float distanceToTarget = 6f; // Distance to target before changing to targeting state
  private float distanceToMainBoss = 6f; // Distance to main boss before changing to returning state

  public MiniBossState CurrentState => currentState;

  public MiniBossStateMachine(MiniBoss miniBoss)
  {
    this.miniBoss = miniBoss;
    currentState = MiniBossState.Orbiting;
    stateTimer = 0f;
  }

  public void Update()
  {
    stateTimer += Time.deltaTime;

    switch (currentState)
    {
      case MiniBossState.Orbiting:
        HandleOrbitingState();
        break;

      case MiniBossState.Targeting:
        HandleTargetingState();
        break;

      case MiniBossState.Returning:
        HandleReturningState();
        break;
    }

    CheckStateTransitions();
  }

  public void ChangeState(MiniBossState newState)
  {
    if (newState == currentState) return;

    Debug.Log($"Mini boss changing state from {currentState} to {newState}");
    currentState = newState;
    stateTimer = 0f;

    // State entry logic
    switch (newState)
    {
      case MiniBossState.Orbiting:
        break;

      case MiniBossState.Targeting:
        Debug.Log("Mini boss targeting player!");
        break;

      case MiniBossState.Returning:
        Debug.Log("Mini boss returning to orbit");
        break;
    }
  }

  private void HandleOrbitingState()
  {
    miniBoss.OrbitAroundBoss();

    // Occasionally attack if player is in range
    if (miniBoss.IsInAttackRange() && stateTimer > 2f)
    {
      miniBoss.AttackPlayer();
    }
  }

  private void HandleTargetingState()
  {
    miniBoss.MoveTowardsPlayer();

    // Attack if in range
    if (miniBoss.IsInAttackRange())
    {
      miniBoss.AttackPlayer();
    }
  }

  private void HandleReturningState()
  {
    miniBoss.ReturnToOrbit();
  }

  private void CheckStateTransitions()
  {
    if (currentState == MiniBossState.Orbiting)
    {
      Debug.Log($"Distance to main boss: {Vector3.Distance(miniBoss.transform.position, miniBoss.MainBoss.transform.position)}");
      Debug.Log($"Is near orbit position: {miniBoss.IsNearOrbitPosition()}");
      Debug.Log($"Distance to player: {Vector3.Distance(miniBoss.transform.position, miniBoss.Player.transform.position)}");
      Debug.Log($"Is in attack range: {miniBoss.IsInAttackRange()}");
      Debug.Log($"State: {currentState}");
      Debug.Log("--------------------------------");
    }

    switch (currentState)
    {
      case MiniBossState.Orbiting:
        if (Vector3.Distance(miniBoss.transform.position, miniBoss.Player.transform.position) < distanceToTarget)
        {
          ChangeState(MiniBossState.Targeting);
        }
        break;

      case MiniBossState.Targeting:
        if (Vector3.Distance(miniBoss.transform.position, miniBoss.MainBoss.transform.position) > distanceToMainBoss)
        {
          ChangeState(MiniBossState.Returning);
        }
        break;

      case MiniBossState.Returning:
        if (miniBoss.IsNearOrbitPosition())
        {
          ChangeState(MiniBossState.Orbiting);
        }
        break;
    }
  }
}
