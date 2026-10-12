using UnityEngine;

public class CharacterIdleState<T> : EntityState<T> where T: Character
{
    public CharacterIdleState(T character): base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Idle, entity.MovementSpeed);
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
