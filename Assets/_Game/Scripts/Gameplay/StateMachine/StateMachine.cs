using UnityEngine;

public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void Initialize(IState state)
    {
        if (state == null) return;

        CurrentState = state;
        CurrentState?.Enter();
    }

    public void ChangeState(IState state)
    {
        if (state == null) return;

        CurrentState?.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
}
