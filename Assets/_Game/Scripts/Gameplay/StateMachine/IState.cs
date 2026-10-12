using UnityEngine;

public interface IState
{
    public abstract void Enter();
    public abstract void Update(float deltaTime);
    public abstract void FixedUpdate(float fixedDeltaTime);
    public abstract void Exit();
}
