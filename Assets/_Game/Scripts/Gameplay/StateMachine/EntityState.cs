using UnityEngine;

public abstract class EntityState<T> : IState where T: Entity
{
    protected readonly T entity;
    public EntityState(T entity)
    {
        this.entity = entity;
    }

    public virtual bool ValidateCondition()
    {
        return true;
    }

    public abstract void Enter();

    public abstract void Update(float deltaTime);

    public abstract void FixedUpdate(float fixedDeltaTime);

    public abstract void Exit();
}
