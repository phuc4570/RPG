using UnityEngine;

public abstract class EntityMovementController
{
    public Rigidbody2D Rb { get; private set; }

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

    public float BaseMovementSpeed { get; protected set; }
    public float MovementSpeed => BaseMovementSpeed;

    public virtual void InitRigidbody(Rigidbody2D rb)
    {
        Rb = rb;
    }

    public virtual void InitBaseMovementSpeed(float baseMovementSpeed)
    {
        BaseMovementSpeed = baseMovementSpeed;
    }

    public virtual void ApplyMovement(Vector2 direction, float speed)
    {
        Rb.linearVelocity = direction * speed;
    }

    public virtual void ApplyForceContinue(Vector2 direction, float force)
    {
        ApplyForce(direction, force);
    }

    public virtual void ApplyForceReset(Vector2 direction, float force)
    {
        StopMovement();
        ApplyForce(direction, force);
    }

    private void ApplyForce(Vector2 direction, float force)
    {
        Rb.linearVelocity = direction * force;
    }

    public virtual void StopMovement()
    {
        Rb.linearVelocity = Vector2.zero;
    }

    public virtual void Reset()
    {
        Target = null;
        StopMovement();
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