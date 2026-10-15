using UnityEngine;

public class MainCharacterHeavyAttackState : EntityState<MainCharacter>
{
    private const float stateDuration = 0.5f;
    private int chargeLevel = 0;
    private float timer;
    private bool isAttackPerformed = false;
    private bool isBlockPerformed = false;
    private bool isDodgePerformed = false;
    private bool isHeavyAttackCharged = false;

    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterHeavyAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        entity.MainCharacterMovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.HeavyAttack, entity.MovementSpeed);

        timer = stateDuration;
        isAttackPerformed = false;
        isBlockPerformed = false;
        isDodgePerformed = false;
        isHeavyAttackCharged = false;

        GameEvent.RaiseVisualAttackFillAnim(stateDuration);

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed += HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged += HandleHeavyAttackCharged;
        mainCharacterInputReader.OnHeavyAttackPerformed += HandleHeavyAttackPerformed;
    }

    public override void Update(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0f)
        {
            if (isDodgePerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
            }
            else if (isBlockPerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.BlockIdle));
            }
            else if (isAttackPerformed)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.LightAttack));
            }
            else if (isHeavyAttackCharged)
            {
                entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.HeavyAttackCharge));
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
        mainCharacterInputReader.OnLightAttackPerformed -= HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged -= HandleHeavyAttackCharged;
        mainCharacterInputReader.OnHeavyAttackPerformed -= HandleHeavyAttackPerformed;
    }

    public void SetChargeLevel(int level)
    {
        chargeLevel = level;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        isBlockPerformed = isBlocking;
    }

    private void HandleDodgePerformed()
    {
        isDodgePerformed = true;
    }

    private void HandleLightAttackPerformed()
    {
        isAttackPerformed = true;
    }

    private void HandleHeavyAttackCharged()
    {
        isHeavyAttackCharged = true;
    }

    private void HandleHeavyAttackPerformed()
    {
        isHeavyAttackCharged = false;
    }
}
