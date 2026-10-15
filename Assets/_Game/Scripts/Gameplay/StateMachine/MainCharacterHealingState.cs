using UnityEngine;

public class MainCharacterHealingState : CharacterHealingState<MainCharacter>
{
    private const float healingDuration = 1f;
    private float healingTimer = 0f;
    public MainCharacterHealingState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override bool ValidateCondition()
    {
        if (entity.HealingPotion.Value > 0)
        {
            entity.HealingPotion.AddValue(-1);
            return true;
        }
        return false;
    }

    public override void Enter()
    {
        base.Enter();
        healingTimer = 0f;
        entity.MainCharacterMovementController.StopMovement();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        healingTimer += deltaTime;
        if (healingTimer >= healingDuration)
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
