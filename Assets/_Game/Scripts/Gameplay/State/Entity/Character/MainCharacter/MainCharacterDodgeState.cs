using UnityEngine;

public class MainCharacterDodgeState : EntityState<MainCharacter>
{
    private const float dodgeDuration = 0.5f;
    private const int staminaCost = 10;
    private float dodgeTimer = 0f;

    private CharacterStateType nextState = CharacterStateType.Idle;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterDodgeState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override bool ValidateCondition()
    {
        if (entity.StaminaSystem.Value >= staminaCost)
        {
            entity.StaminaSystem.AddValue(-staminaCost);
            return true;
        }
        return false;
    }

    public override void Enter()
    {
        Vector2 input = mainCharacterInputReader.MoveInput;
        entity.Direction = input.normalized;
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, dodgeDuration);
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dodge, entity.MovementSpeed);
        entity.MainCharacterMovementController.ApplyDodgeImpulse(entity.Direction, entity.DodgeForce);
        dodgeTimer = 0f;
        //if (entity.CombatDetector.IsBossAttackingInDodgeWindow())
        //{
        //    TriggerPerfectDodge();
        //}
        nextState = mainCharacterInputReader.IsBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;

        Physics2D.IgnoreLayerCollision(entity.gameObject.layer, entity.AttackLayerMaskInLog2, true);

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
    }

    public override void Update(float deltaTime)
    {
        dodgeTimer += deltaTime;

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
        Physics2D.IgnoreLayerCollision(entity.gameObject.layer, entity.AttackLayerMaskInLog2, false);
        mainCharacterInputReader.OnBlockPerformed -= HandleBlockPerformed;
    }

    //private void TriggerPerfectDodge()
    //{
    //    // Kích hoạt Slow-motion thế giới (0.2x speed) trong 1.5s thực tế
    //    // (Dùng TimeManager đã dựng ở câu trước)
    //    TimeManager.Instance.TriggerSelectiveSlowMo(worldSlowFactor: 0.2f, duration: 1.5f);

    //    // Phát VFX / SFX thông báo Perfect Dodge thành công
    //    entity.VFXController.PlayPerfectDodgeVFX();
    //    entity.AudioController.PlayPerfectDodgeSFX();
    //}

    private void HandleBlockPerformed(bool isBlocking)
    {
        nextState = isBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;
    }
}
