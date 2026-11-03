using UnityEngine;

public class CharacterHealingState<T> : EntityState<T> where T : Character
{
    protected virtual float StateDuration => 0f;
    private float stateTimer = 0f;

    public CharacterHealingState(T character) : base(character)
    {

    }

    public override void Enter()
    {
        stateTimer = 0f;
        entity.VFXController.PlayVFX(VFXTypeEnum.Healing, Vector3.one);
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Healing, entity.MovementController.MovementSpeed);
        SoundManager.Ins.PlaySound(SoundTypeEnum.Healing);
    }

    public override void Update(float deltaTime)
    {
        stateTimer += deltaTime;
        if (stateTimer >= StateDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {
        entity.VFXController.StopVFX(VFXTypeEnum.Healing);
    }
}
