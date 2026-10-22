using UnityEngine;

public abstract class EntityPhysicsController
{
    protected Collider2D MainCollider { get; private set; }
    protected Collider2D HitBox { get; private set; }

    public virtual void Init(Collider2D mainCollider, Collider2D hitBox)
    {
        MainCollider = mainCollider;
        HitBox = hitBox;
    }

    public virtual void ActiveAllColliders(bool flag)
    {
        ActiveMainCollider(flag);
        ActiveHitBoxCollider(flag);
    }

    public virtual void ActiveMainCollider(bool flag)
    {
        MainCollider.enabled = flag;
    }

    public virtual void ActiveHitBoxCollider(bool flag)
    {
        HitBox.enabled = flag;
    }
}

public abstract class EntityPhysicsController<T> : EntityPhysicsController where T : Entity
{
    public T Entity { get; }
    protected EntityPhysicsController(T entity)
    {
        Entity = entity;
    }
}