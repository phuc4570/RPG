public abstract class EntityCombatController
{

}

public abstract class EntityCombatController<T> : EntityCombatController where T : Entity
{
    public T Entity { get; }
    protected EntityCombatController(T entity)
    {
        Entity = entity;
    }
}