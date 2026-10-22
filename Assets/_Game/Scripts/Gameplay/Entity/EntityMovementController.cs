using UnityEngine;

public abstract class EntityMovementController
{
    protected Rigidbody2D Rb { get; private set; }

    private GameObject target;
    public GameObject Target 
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    public virtual void Init(Rigidbody2D rb)
    {
        Rb = rb;
    }

    public virtual void ApplyMovement(Vector2 moveInput, float moveSpeed)
    {
        Rb.linearVelocity = moveInput * moveSpeed;
    }

    public virtual void StopMovement()
    {
        Rb.linearVelocity = Vector2.zero;
    }
}

public abstract class EntityMovementController<T> : EntityMovementController where T : Entity
{
    public T Entity { get; }
    protected EntityMovementController(T entity)
    {
        Entity = entity;
    }
}