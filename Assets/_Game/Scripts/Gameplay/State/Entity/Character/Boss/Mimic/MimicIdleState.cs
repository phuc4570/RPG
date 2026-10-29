using UnityEngine;

public class MimicIdleState : CharacterIdleState<Mimic>
{
    public MimicIdleState(Mimic mimic) : base(mimic)
    {

    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (entity.EnemyCombatController.IsInHitRange)
        {
            if (!entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack)))
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
            }
        }
        else
        {
            Vector2 movementDirection = entity.MovementController.Target != null ? (entity.MovementController.Target.transform.position - entity.transform.position).normalized : Vector2.zero;
            entity.Direction = movementDirection;
            if (movementDirection.sqrMagnitude > 0.001f)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Moving));
            }
        }

    }
}
