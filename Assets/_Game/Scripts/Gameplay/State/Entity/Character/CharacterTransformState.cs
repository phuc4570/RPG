using UnityEngine;

public class CharacterTransformState<T> : EntityState<T> where T : Character
{
    protected virtual float StateDuration => 0f;
    private float stateTimer = 0f;

    public CharacterTransformState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        stateTimer = 0f;
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Transform, entity.MovementController.MovementSpeed);
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, StateDuration);
    }

    public override void Update(float deltaTime)
    {
        stateTimer += deltaTime;
        if (stateTimer >= StateDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {

    }
}