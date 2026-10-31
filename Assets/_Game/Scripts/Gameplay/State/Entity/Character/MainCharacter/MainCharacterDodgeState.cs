using UnityEngine;

public class MainCharacterDodgeState : EntityState<MainCharacter>
{
    private const float perfectDodgeWindow = 0.2f;
    private const float dodgeDuration = 0.5f;
    private float dodgeTimer = 0f;

    private CharacterStateType nextState = CharacterStateType.Idle;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterDodgeState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override bool ValidateCondition()
    {
        if (entity.StaminaSystem.Value >= entity.MainCharacterCombatController.DodgeStaminaCost)
        {
            entity.StaminaSystem.AddValue(-entity.MainCharacterCombatController.DodgeStaminaCost);
            return true;
        }
        return false;
    }

    public override void Enter()
    {
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;
        entity.MainCharacterCombatController.IsPerfectDodge = true;
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, dodgeDuration);
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dodge, entity.MovementController.MovementSpeed);
        entity.MainCharacterMovementController.ApplyForceReset(entity.Direction, entity.MainCharacterCombatController.DodgeForce);
        dodgeTimer = 0f;
        nextState = mainCharacterInputReader.IsBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;

        Physics2D.IgnoreLayerCollision(entity.gameObject.layer, entity.CharacterCombatController.AttackLayerMaskInLog2, true);

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
    }

    public override void Update(float deltaTime)
    {
        dodgeTimer += deltaTime;

        if (dodgeTimer >= perfectDodgeWindow)
        {
            entity.MainCharacterCombatController.IsPerfectDodge = false;
        }

        if (dodgeTimer >= dodgeDuration)
        {
            IState nextStateInterface;
            switch (nextState)
            {
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
        entity.MainCharacterCombatController.IsPerfectDodge = false;
        Physics2D.IgnoreLayerCollision(entity.gameObject.layer, entity.CharacterCombatController.AttackLayerMaskInLog2, false);
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        nextState = isBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;
    }
}
