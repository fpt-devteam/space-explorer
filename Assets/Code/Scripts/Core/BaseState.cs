using UnityEngine;

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
  protected virtual void OnEnter(T context) { }
  protected virtual void OnUpdate(T context) { }
  protected virtual void OnExit(T context) { }
  protected virtual IState<T> OnCheckTransitions(T context) { return null; }
  protected float StateTime => stateTime;
}
    