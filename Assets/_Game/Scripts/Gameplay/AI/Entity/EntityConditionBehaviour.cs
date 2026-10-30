public class EntityConditionBehaviour<T> : EntityBehaviour<T> where T : Entity
{
    private readonly System.Func<T, BehaviourStateEnum> onEvaluate;

    public EntityConditionBehaviour(T entity, System.Func<T, BehaviourStateEnum> onEvaluate) : base(entity)
    {
        this.onEvaluate = onEvaluate;
    }

    public override BehaviourStateEnum Evaluate()
    {
        return onEvaluate?.Invoke(Entity) ?? BehaviourStateEnum.Failure;
    }
}