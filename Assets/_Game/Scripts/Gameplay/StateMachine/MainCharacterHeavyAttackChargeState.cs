using UnityEngine;

public class MainCharacterHeavyAttackChargeState : EntityState<MainCharacter>
{
    private const float chargeTimePerPoint = 1.25f;
    private float chargeTimer = 0f;
    private int chargePoints = 0;

    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterHeavyAttackChargeState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        chargeTimer = 0f;
        chargePoints = 0;

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnHeavyAttackPerformed += HandleHeavyAttackPerformed;
    }

    public override void Update(float deltaTime)
    {
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;
        if (input.sqrMagnitude <= 0.001f)
        {
            entity.MainCharacterMovementController.StopMovement();
            if(entity.AnimationController.GetCurrentAnimation<CharacterAnimationState>() != CharacterAnimationState.Idle)
            {
                entity.AnimationController.PlayAnimation(CharacterAnimationState.Idle, entity.MovementSpeed);
            }
        }
        else
        {
            entity.MainCharacterMovementController.ApplyMovement(input, entity.MovementSpeed);
            if (entity.AnimationController.GetCurrentAnimation<CharacterAnimationState>() != CharacterAnimationState.Moving)
            {
                entity.AnimationController.PlayAnimation(CharacterAnimationState.Moving, entity.MovementSpeed);
            }
        }

        chargeTimer += Time.deltaTime;

        if(chargeTimer >= chargeTimePerPoint)
        {
            chargeTimer -= chargeTimePerPoint;
            if (entity.MainCharacterFocusSystem.ConsumeFocus())
            {
                chargePoints++;
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
        mainCharacterInputReader.OnHeavyAttackPerformed -= HandleHeavyAttackPerformed;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        if (isBlocking)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.BlockIdle));
            chargePoints = 0;
        }
    }

    private void HandleDodgePerformed()
    {
        entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Dodge));
        chargePoints = 0;
    }

    private void HandleHeavyAttackPerformed()
    {
        var heavyAttackState = entity.StateFactory.GetState(CharacterStateType.HeavyAttack) as MainCharacterHeavyAttackState;
        heavyAttackState.SetChargeLevel(chargePoints);
        entity.StateMachine.ChangeState(heavyAttackState);
        chargePoints = 0;
    }
}
