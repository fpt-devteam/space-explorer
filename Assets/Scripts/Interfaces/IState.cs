using UnityEngine;

/// <summary>
/// Generic state interface for any state machine implementation
/// </summary>
/// <typeparam name="T">The context type that owns this state machine</typeparam>
public interface IState<T>
{
  /// <summary>
  /// Called when entering this state
  /// </summary>
  /// <param name="context">The context object (owner of the state machine)</param>
  void Enter(T context);

  /// <summary>
  /// Called every frame while in this state
  /// </summary>
  /// <param name="context">The context object</param>
  void Update(T context);

  /// <summary>
  /// Called when exiting this state
  /// </summary>
  /// <param name="context">The context object</param>
  void Exit(T context);

  /// <summary>
  /// Check if this state should transition to another state
  /// </summary>
  /// <param name="context">The context object</param>
  /// <returns>The next state to transition to, or null if no transition</returns>
  IState<T> CheckTransitions(T context);
}
