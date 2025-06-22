using UnityEngine;

/// <summary>
/// Abstract base state that provides common functionality
/// Inherit from this for most state implementations
/// </summary>
/// <typeparam name="T">The context type</typeparam>
public abstract class BaseState<T> : IState<T>
{
  protected float stateTime;
  protected bool hasEnteredState;

  public virtual void Enter(T context)
  {
    stateTime = 0f;
    hasEnteredState = true;
    OnEnter(context);
  }

  public virtual void Update(T context)
  {
    if (!hasEnteredState) return;

    stateTime += Time.deltaTime;
    OnUpdate(context);
  }

  public virtual void Exit(T context)
  {
    hasEnteredState = false;
    OnExit(context);
  }

  public virtual IState<T> CheckTransitions(T context)
  {
    return OnCheckTransitions(context);
  }

  /// <summary>
  /// Override this for state-specific enter logic
  /// </summary>
  protected virtual void OnEnter(T context) { }

  /// <summary>
  /// Override this for state-specific update logic
  /// </summary>
  protected virtual void OnUpdate(T context) { }

  /// <summary>
  /// Override this for state-specific exit logic
  /// </summary>
  protected virtual void OnExit(T context) { }

  /// <summary>
  /// Override this for state-specific transition logic
  /// </summary>
  /// <returns>Next state to transition to, or null if no transition</returns>
  protected virtual IState<T> OnCheckTransitions(T context) { return null; }

  /// <summary>
  /// Helper property to get time spent in this state
  /// </summary>
  protected float StateTime => stateTime;
}
