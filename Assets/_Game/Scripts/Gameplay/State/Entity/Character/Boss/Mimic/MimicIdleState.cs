using UnityEngine;

public class MimicIdleState : CharacterIdleState<Mimic>
{
    private Mimic.MimicState currentMimicState;

    public MimicIdleState(Mimic mimic) : base(mimic)
    {

    }

    public override void Enter()
    {
        base.Enter();
        SwitchStateAnim();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if(currentMimicState != entity.State)
        {
            SwitchStateAnim();
        }

        if(entity.State == Mimic.MimicState.Transformed)
        {
            if (entity.BossCombatController.AttackCount < 3 && entity.BossCombatController.IsInHitRange(CharacterAttackTypeEnum.LightAttack))
            {
                if (!entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack)))
                {
                    entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
                }
            }
            else if(entity.BossCombatController.AttackCount >= 3 && entity.BossCombatController.IsInHitRange(CharacterAttackTypeEnum.HeavyAttack))
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
                if (movementDirection.sqrMagnitude > 0.001f)
                {
                    entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Moving));
                }
            }
        }
    }

    private void SwitchStateAnim()
    {
        currentMimicState = entity.State;
        switch (entity.State)
        {
            case Mimic.MimicState.Close:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.Idle, entity.MovementController.MovementSpeed);
                break;
            case Mimic.MimicState.Open:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.Idle1, entity.MovementController.MovementSpeed);
                break;
            case Mimic.MimicState.Transformed:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.Idle2, entity.MovementController.MovementSpeed);
                break;
        }
    }
}
