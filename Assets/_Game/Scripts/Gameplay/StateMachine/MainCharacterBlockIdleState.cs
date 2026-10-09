using UnityEngine;

public class MainCharacterBlockIdleState : EntityState<MainCharacter>
{
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterBlockIdleState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        entity.IsBlocking = true;
        entity.MainCharacterMovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.BlockIdle, entity.MovementSpeed);
        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        entity.IsBlocking = false;
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed -= HandleDodgePerformed;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        if (!isBlocking)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    private void HandleDodgePerformed()
    {
        entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
    }
}
