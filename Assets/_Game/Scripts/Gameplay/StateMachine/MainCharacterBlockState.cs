using UnityEngine;

public class MainCharacterBlockState : EntityState<MainCharacter>
{
    private const float blockDuration = 0.5f;
    private float blockTimer = 0f;
    private CharacterStateType nextState = CharacterStateType.Idle;
    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterBlockState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        blockTimer = 0f;
        nextState = CharacterStateType.Idle;
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
            IState nextStateInterface;
            switch (nextState)
            {
                case CharacterStateType.Dodge:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.Dodge);
                    break;
                case CharacterStateType.BlockIdle:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.BlockIdle);
                    break;
                default:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.Idle);
                    break;
            }

            if (!entity.StateMachine.ChangeState(nextStateInterface))
            {
                nextStateInterface = entity.StateFactory.GetState(CharacterStateType.Idle);
                entity.StateMachine.ChangeState(nextStateInterface);
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
        nextState = isBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;
    }

    private void HandleDodgePerformed()
    {
        nextState = CharacterStateType.Dodge;
    }
}
