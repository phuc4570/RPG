using UnityEngine;

public class MimicMovingState : CharacterMovingState<Mimic>
{
    public MimicMovingState(Mimic mimic) : base(mimic)
    {

    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        if (entity.State == Mimic.MimicState.Transformed)
        {
            if (entity.BossCombatController.AttackCount < 3 && entity.BossCombatController.IsInHitRange(CharacterAttackTypeEnum.LightAttack))
            {
                if (!entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack)))
                {
                    entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
                }
            }
            else if (entity.BossCombatController.AttackCount >= 3 && entity.BossCombatController.IsInHitRange(CharacterAttackTypeEnum.HeavyAttack))
            {
                if (!entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.HeavyAttack)))
                {
                    entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
                }
            }
            else
            {
                Vector2 movementDirection = entity.MovementController.Target != null ? (entity.MovementController.Target.transform.position - entity.transform.position).normalized : Vector2.zero;
                entity.Direction = movementDirection;
                if (movementDirection.sqrMagnitude <= 0.001f)
                {
                    entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
                    return;
                }
                entity.MovementController.ApplyMovement(movementDirection, entity.MovementController.MovementSpeed);
            }
        }
    }
}
