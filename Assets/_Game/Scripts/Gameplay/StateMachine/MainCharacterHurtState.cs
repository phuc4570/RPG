using UnityEngine;

public class MainCharacterHurtState : CharacterHurtState<MainCharacter>
{
    private const float hurtDuration = 0.25f;
    private float hurtTimer = 0f;
    public MainCharacterHurtState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override void Enter()
    {
        base.Enter();
        hurtTimer = 0f;
        entity.MainCharacterMovementController.StopMovement();
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, hurtDuration);
        //GameEvent.RaiseMainCharacterHurt();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        hurtTimer += deltaTime;
        if(hurtTimer >= hurtDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        base.FixedUpdate(fixedDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
