using DG.Tweening;
using UnityEngine;

public class CharacterDeadState<T> : EntityState<T> where T : Character
{
    protected const float delayDuration = 1f;
    protected virtual float StateDuration => 0f;
    protected float stateTimer = 0f;

    public CharacterDeadState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        stateTimer = 0f;
        entity.PreDisableEntity();
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dead, entity.MovementController.MovementSpeed);
    }

    public override void Update(float deltaTime)
    {
        stateTimer += deltaTime;
        if(stateTimer >= StateDuration + delayDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Disappear));
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {

    }
}
