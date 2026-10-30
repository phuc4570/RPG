using System.Collections.Generic;

public class EntitySequenceBehaviour<T> : EntityBehaviour<T> where T : Entity
{
    private List<EntityBehaviour<T>> behaviours;
    public EntitySequenceBehaviour(T entity, List<EntityBehaviour<T>> entityBehaviours) : base(entity)
    {
        behaviours = entityBehaviours;
    }

    public override BehaviourStateEnum Evaluate()
    {
        for (int i = 0; i < behaviours.Count; i++)
        {
            var result = behaviours[i].Evaluate();
            if (result != BehaviourStateEnum.Success)
                return result;
        }
        return BehaviourStateEnum.Success;
    }
}