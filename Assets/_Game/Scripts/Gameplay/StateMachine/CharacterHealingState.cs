using UnityEngine;

public class CharacterHealingState<T> : EntityState<T> where T : Character
{
    public CharacterHealingState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Healing, entity.MovementSpeed);
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
