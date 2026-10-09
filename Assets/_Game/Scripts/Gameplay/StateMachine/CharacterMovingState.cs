using UnityEngine;

public class CharacterMovingState<T> : EntityState<T> where T : Character
{
    public CharacterMovingState(T character) : base(character)
    {
    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Moving, entity.MovementSpeed);
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
