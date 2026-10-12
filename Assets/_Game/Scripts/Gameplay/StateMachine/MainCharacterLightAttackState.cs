using UnityEngine;

public class MainCharacterLightAttackState : EntityState<MainCharacter>
{
    private const float comboResetTime = 0.5f;
    private const float stateDuration = 0.4f;

    private int comboIdx = 0;
    private float lastAttackTime;
    private float timer;
    private bool isAttackPerformed = false;
    private bool isBlockPerformed = false;
    private bool isDodgePerformed = false;
    private bool isHeavyAttackCharged = false;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterLightAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }
    public override void Enter()
    {
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;

        if (lastAttackTime + comboResetTime < Time.time)
        {
            comboIdx = 0;
        }

        entity.MainCharacterMovementController.StopMovement();
        switch (comboIdx)
        {
            case 0:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack1, entity.MovementSpeed);
                break;
            case 1:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack2, entity.MovementSpeed);
                break;
            case 2:
                entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack3, entity.MovementSpeed);
                break;
        }

        timer = stateDuration;
        isAttackPerformed = false;
        isBlockPerformed = false;
        isDodgePerformed = false;
        isHeavyAttackCharged = false;

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
            lastAttackTime = Time.time;
            comboIdx = (comboIdx + 1) % 3;

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
