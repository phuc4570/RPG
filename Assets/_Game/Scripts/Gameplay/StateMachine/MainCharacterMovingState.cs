using UnityEngine;

public class MainCharacterMovingState : CharacterMovingState<MainCharacter>
{
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterMovingState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }
    public override void Enter()
    {
        base.Enter();
        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed += HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged += HandleHeavyAttackCharged;
    }

    public override void Update()
    {
        base.Update();
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;
        if (input.sqrMagnitude <= 0.001f)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
            return;
        }
        entity.MainCharacterMovementController.ApplyMovement(input, entity.MovementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed -= HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed -= HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged -= HandleHeavyAttackCharged;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        if (isBlocking)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.BlockIdle));
        }
    }

    private void HandleDodgePerformed()
    {
        entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
    }

    private void HandleLightAttackPerformed()
    {
        entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack));
    }

    private void HandleHeavyAttackCharged()
    {
        entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.HeavyAttackCharge));
    }
}
