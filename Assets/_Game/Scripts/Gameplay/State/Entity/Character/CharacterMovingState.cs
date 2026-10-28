using UnityEngine;

public class CharacterMovingState<T> : EntityState<T> where T : Character
{
    public CharacterMovingState(T character) : base(character)
    {
    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Moving, entity.MovementController.MovementSpeed);
    }

    public override void Update(float deltaTime)
    {

    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {

    }
}
