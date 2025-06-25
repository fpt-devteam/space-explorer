using UnityEngine;

/// <summary>
/// Example usage of the generic state machine system
/// This shows how you can use the same state machine for different purposes
/// </summary>

// EXAMPLE 1: Simple Player States
public enum PlayerStateType { Idle, Moving, Attacking, Stunned }

public class PlayerStateController : StateMachineController<PlayerStateController>
{
  [Header("Player State Example")]
  public PlayerStateType currentStateType;

  // State instances
  private PlayerIdleState idleState;
  private PlayerMovingState movingState;
  private PlayerAttackingState attackingState;
  private PlayerStunnedState stunnedState;

  protected override void Awake()
  {
    base.Awake();

    // Initialize states
    idleState = new PlayerIdleState();
    movingState = new PlayerMovingState();
    attackingState = new PlayerAttackingState();
    stunnedState = new PlayerStunnedState();
  }

  protected override void InitializeStateMachine()
  {
    stateMachine.Initialize(this, idleState);
  }

  protected override void Update()
  {
    base.Update();

    // Update current state type for debugging
    if (IsInState<PlayerIdleState>()) currentStateType = PlayerStateType.Idle;
    else if (IsInState<PlayerMovingState>()) currentStateType = PlayerStateType.Moving;
    else if (IsInState<PlayerAttackingState>()) currentStateType = PlayerStateType.Attacking;
    else if (IsInState<PlayerStunnedState>()) currentStateType = PlayerStateType.Stunned;
  }

  // Public methods for external state changes
  public void Stun(float duration) => ((PlayerStunnedState)stunnedState).SetStunDuration(duration);

  public PlayerIdleState GetIdleState() => idleState;
  public PlayerMovingState GetMovingState() => movingState;
  public PlayerAttackingState GetAttackingState() => attackingState;
  public PlayerStunnedState GetStunnedState() => stunnedState;
}

// EXAMPLE STATES FOR PLAYER
public class PlayerIdleState : BaseState<PlayerStateController>
{
  protected override void OnEnter(PlayerStateController player)
  {
    Debug.Log("Player entering idle state");
  }

  protected override IState<PlayerStateController> OnCheckTransitions(PlayerStateController player)
  {
    // Example transition logic
    if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
      return player.GetMovingState();

    if (Input.GetButtonDown("Fire1"))
      return player.GetAttackingState();

    return null;
  }
}

public class PlayerMovingState : BaseState<PlayerStateController>
{
  protected override void OnEnter(PlayerStateController player)
  {
    Debug.Log("Player entering moving state");
  }

  protected override void OnUpdate(PlayerStateController player)
  {
    // Handle movement logic here
  }

  protected override IState<PlayerStateController> OnCheckTransitions(PlayerStateController player)
  {
    if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
      return player.GetIdleState();

    if (Input.GetButtonDown("Fire1"))
      return player.GetAttackingState();

    return null;
  }
}

public class PlayerAttackingState : BaseState<PlayerStateController>
{
  private float attackDuration = 0.5f;

  protected override void OnEnter(PlayerStateController player)
  {
    Debug.Log("Player attacking!");
    // Trigger attack animation/effects
  }

  protected override IState<PlayerStateController> OnCheckTransitions(PlayerStateController player)
  {
    if (StateTime >= attackDuration)
      return player.GetIdleState();

    return null;
  }
}

public class PlayerStunnedState : BaseState<PlayerStateController>
{
  private float stunDuration = 2f;

  public void SetStunDuration(float duration) => stunDuration = duration;

  protected override void OnEnter(PlayerStateController player)
  {
    Debug.Log($"Player stunned for {stunDuration} seconds!");
  }

  protected override IState<PlayerStateController> OnCheckTransitions(PlayerStateController player)
  {
    if (StateTime >= stunDuration)
      return player.GetIdleState();

    return null;
  }
}

public class GameFlowController : StateMachineController<GameFlowController>
{
  private MainMenuState mainMenuState;
  private GameplayState gameplayState;
  private PausedState pausedState;
  private GameOverState gameOverState;

  protected override void Awake()
  {
    base.Awake();
    mainMenuState = new MainMenuState();
    gameplayState = new GameplayState();
    pausedState = new PausedState();
    gameOverState = new GameOverState();
  }

  protected override void InitializeStateMachine()
  {
    stateMachine.Initialize(this, mainMenuState);
  }

  public MainMenuState GetMainMenuState() => mainMenuState;
  public GameplayState GetGameplayState() => gameplayState;
  public PausedState GetPausedState() => pausedState;
  public GameOverState GetGameOverState() => gameOverState;
}

public class MainMenuState : BaseState<GameFlowController> { }
public class GameplayState : BaseState<GameFlowController> { }
public class PausedState : BaseState<GameFlowController> { }
public class GameOverState : BaseState<GameFlowController> { }