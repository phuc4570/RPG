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
