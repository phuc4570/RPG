using UnityEngine;

public class MainCharacterBlockIdleState : EntityState<MainCharacter>
{
    private const float parryWindow = 0.1f;
    private float timer = 0f;
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterBlockIdleState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        entity.MainCharacterCombatController.IsBlocking = true;
        entity.MainCharacterCombatController.IsPerfectBlock = true;
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.BlockIdle, entity.MovementController.MovementSpeed);
        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;

        timer = 0f;
    }

    public override void Update(float deltaTime)
    {
        if (entity.MainCharacterCombatController.IsPerfectBlock)
        {
            timer += deltaTime;
            if (timer >= parryWindow)
            {
                entity.MainCharacterCombatController.IsPerfectBlock = false;
            }
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {
        entity.MainCharacterCombatController.IsBlocking = false;
        entity.MainCharacterCombatController.IsPerfectBlock = false;
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
