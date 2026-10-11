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
