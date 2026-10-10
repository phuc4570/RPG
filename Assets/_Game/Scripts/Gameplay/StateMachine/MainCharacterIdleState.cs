using UnityEngine;

public class MainCharacterIdleState : CharacterIdleState<MainCharacter>
{
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterIdleState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        base.Enter();
        entity.MainCharacterMovementController.StopMovement();
        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed += HandleLightAttackPerformed;
    }

    public override void Update()
    {
        base.Update();
        if(mainCharacterInputReader.MoveInput.sqrMagnitude > 0.001f)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Moving));
        }
    }

    public override void Exit()
    {
        base.Exit();
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed -= HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed -= HandleLightAttackPerformed;
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
}
