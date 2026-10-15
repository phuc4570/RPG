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

    public bool ChangeState(IState state)
    {
        if (state == null) return false;

        if (state.ValidateCondition())
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
            return true;
        }
        return false;
    }
}
