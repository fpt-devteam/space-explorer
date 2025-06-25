using UnityEngine;

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

    if (showDebugInfo && stateMachine?.CurrentState != null)
    {
      currentStateName = stateMachine.CurrentState.GetType().Name;
    }
  }

  protected abstract void InitializeStateMachine();

  public IState<T> GetCurrentState()
  {
    return stateMachine?.CurrentState;
  }

  public void ChangeState(IState<T> newState)
  {
    stateMachine?.ChangeState(newState);
  }

  public bool IsInState<TState>() where TState : IState<T>
  {
    return stateMachine?.CurrentState is TState;
  }

  protected virtual T GetContext()
  {
    return this as T;
  }
}
