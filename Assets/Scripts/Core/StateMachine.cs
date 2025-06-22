using UnityEngine;

/// <summary>
/// Generic state machine that can be used for any context type
/// </summary>
/// <typeparam name="T">The context type that owns this state machine</typeparam>
public class StateMachine<T>
{
  private IState<T> currentState;
  private T context;

  public IState<T> CurrentState => currentState;

  /// <summary>
  /// Initialize the state machine with a context and initial state
  /// </summary>
  /// <param name="context">The context object that owns this state machine</param>
  /// <param name="initialState">The initial state to start with</param>
  public void Initialize(T context, IState<T> initialState)
  {
    this.context = context;
    ChangeState(initialState);
  }

  /// <summary>
  /// Update the state machine - should be called every frame
  /// </summary>
  public void Update()
  {
    if (currentState == null) return;

    // Update current state
    currentState.Update(context);

    // Check for transitions
    IState<T> nextState = currentState.CheckTransitions(context);
    if (nextState != null && nextState != currentState)
    {
      ChangeState(nextState);
    }
  }

  /// <summary>
  /// Manually change to a new state
  /// </summary>
  /// <param name="newState">The new state to transition to</param>
  public void ChangeState(IState<T> newState)
  {
    if (newState == null) return;

    // Exit current state
    currentState?.Exit(context);

    // Change to new state
    currentState = newState;

    // Enter new state
    currentState.Enter(context);

    Debug.Log($"State changed to: {currentState.GetType().Name}");
  }

  /// <summary>
  /// Force transition to a specific state without calling CheckTransitions
  /// </summary>
  /// <param name="newState">The state to force transition to</param>
  public void ForceState(IState<T> newState)
  {
    ChangeState(newState);
  }
}
