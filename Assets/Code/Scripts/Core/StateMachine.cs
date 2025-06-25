using UnityEngine;

public class StateMachine<T>
{
  private IState<T> currentState;
  private T context;
  public IState<T> CurrentState => currentState;

  public void Initialize(T context, IState<T> initialState)
  {
    this.context = context;
    ChangeState(initialState);
  }
  public void Update()
  {
    if (currentState == null) return;

    currentState.Update(context);
    IState<T> nextState = currentState.CheckTransitions(context);

    if (nextState != null && nextState != currentState)
    {
      ChangeState(nextState);
    }
  }
  public void ChangeState(IState<T> newState)
  {
    if (newState == null) return;

    currentState?.Exit(context);
    currentState = newState;
    currentState.Enter(context);
    Debug.Log($"State changed to: {currentState.GetType().Name}");
  }
  public void ForceState(IState<T> newState)
  {
    ChangeState(newState);
  }
}
