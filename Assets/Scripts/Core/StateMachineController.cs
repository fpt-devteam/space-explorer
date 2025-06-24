using UnityEngine;

/// <summary>
/// MonoBehaviour wrapper for the generic state machine
/// Inherit from this to create state machine-controlled components
/// </summary>
/// <typeparam name="T">The context type (usually the inheriting class itself)</typeparam>
public abstract class StateMachineController<T> : MonoBehaviour where T : class
{
  protected StateMachine<T> stateMachine;

  [Header("State Machine Debug")]
  [SerializeField] private bool showDebugInfo = true;
  [SerializeField] private string currentStateName = "None";

  protected virtual void Awake()
  {
    stateMachine = new StateMachine<T>();
  }

  protected virtual void Start()
  {
    InitializeStateMachine();
  }

  protected virtual void Update()
  {
    stateMachine?.Update();

    // Update debug info
    if (showDebugInfo && stateMachine?.CurrentState != null)
    {
      currentStateName = stateMachine.CurrentState.GetType().Name;
    }
  }

  /// <summary>
  /// Override this to set up your initial state
  /// </summary>
  protected abstract void InitializeStateMachine();

  /// <summary>
  /// Get the current state
  /// </summary>
  public IState<T> GetCurrentState()
  {
    return stateMachine?.CurrentState;
  }

  /// <summary>
  /// Force transition to a specific state
  /// </summary>
  public void ChangeState(IState<T> newState)
  {
    stateMachine?.ChangeState(newState);
  }

  /// <summary>
  /// Check if currently in a specific state type
  /// </summary>
  public bool IsInState<TState>() where TState : IState<T>
  {
    return stateMachine?.CurrentState is TState;
  }

  /// <summary>
  /// Get the context for this state machine (usually 'this')
  /// </summary>
  protected virtual T GetContext()
  {
    return this as T;
  }
}
