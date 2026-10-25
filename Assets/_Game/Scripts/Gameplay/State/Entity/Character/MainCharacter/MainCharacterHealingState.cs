using UnityEngine;

public class MainCharacterHealingState : CharacterHealingState<MainCharacter>
{
    protected override float StateDuration => 1f;
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
        entity.MovementController.StopMovement();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
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
