using UnityEngine;

public class MainCharacterBlockState : EntityState<MainCharacter>
{
    private const float blockDuration = 0.5f;
    private float blockTimer = 0f;
    private bool isBlockPerformed = false;
    private bool isDodgePerformed = false;
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterBlockState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        blockTimer = 0f;
        entity.MainCharacterMovementController.StopMovement();
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, blockDuration);
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Block, entity.MovementSpeed);
        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
    }

    public override void Update(float deltaTime)
    {
        blockTimer += deltaTime;
        if(blockTimer >= blockDuration)
        {
            if (isDodgePerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
            }
            else if (isBlockPerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.BlockIdle));
            }
            else
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
            }
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed -= HandleDodgePerformed;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        isBlockPerformed = isBlocking;
    }

    private void HandleDodgePerformed()
    {
        isDodgePerformed = true;
    }
}
