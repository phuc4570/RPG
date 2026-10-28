using UnityEngine;

public class CharacterStunnedState<T> : EntityState<T> where T : Character
{
    public CharacterStunnedState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Stunned, entity.MovementController.MovementSpeed);
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