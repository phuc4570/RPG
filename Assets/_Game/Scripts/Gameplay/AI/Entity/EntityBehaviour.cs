public abstract class EntityBehaviour<T> where T : Entity
{
    protected T Entity { get; private set; }
    protected EntityBehaviour(T entity)
    {
        Entity = entity;
    }
    public abstract BehaviourStateEnum Evaluate();
}
