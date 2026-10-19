using UnityEngine;

public class CharacterHurtState<T> : EntityState<T> where T : Character
{
    protected virtual float StateDuration => 0f;
    private float stateTimer = 0f;

    public CharacterHurtState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        stateTimer = 0f;
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Hurt, entity.MovementSpeed);
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
