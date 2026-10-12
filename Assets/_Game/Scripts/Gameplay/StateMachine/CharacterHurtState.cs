using UnityEngine;

public class CharacterHurtState<T> : EntityState<T> where T : Character
{
    public CharacterHurtState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Hurt, entity.MovementSpeed);
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
