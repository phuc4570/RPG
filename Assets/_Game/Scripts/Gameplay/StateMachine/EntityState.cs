using UnityEngine;

public abstract class EntityState<T> : IState where T: Entity
{
    protected readonly T entity;
    public EntityState(T entity)
    {
        this.entity = entity;
    }
    public abstract void Enter();

    public abstract void Update();

    public abstract void FixedUpdate();

    public abstract void Exit();
}
