using UnityEngine;

public class CharacterDeadState<T> : EntityState<T> where T : Character
{
    protected virtual float StateDuration => 0f;
    private float stateTimer = 0f;

    public CharacterDeadState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        stateTimer = 0f;
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dead, entity.MovementSpeed);
    }

    public override void Update(float deltaTime)
    {
        stateTimer += deltaTime;
        if(stateTimer >= StateDuration)
        {
            entity.DisableEntity();
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {

    }
}
