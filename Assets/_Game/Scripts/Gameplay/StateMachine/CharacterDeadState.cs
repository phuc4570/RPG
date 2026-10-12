using UnityEngine;

public class CharacterDeadState<T> : EntityState<T> where T : Character
{
    public CharacterDeadState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dead, entity.MovementSpeed);
        //entity.DisableCharacter();
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
