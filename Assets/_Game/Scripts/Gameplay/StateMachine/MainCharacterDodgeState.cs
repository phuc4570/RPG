using UnityEngine;

public class MainCharacterDodgeState : EntityState<MainCharacter>
{
    private const float dodgeDuration = 0.5f;
    private float dodgeTimer = 0f;

    private bool isBlockPerformed = false;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterDodgeState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
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
        isBlockPerformed = mainCharacterInputReader.IsBlocking;

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
    }

    public override void Update(float deltaTime)
    {
        dodgeTimer += deltaTime;

        if (dodgeTimer >= dodgeDuration)
        {
            if (isBlockPerformed)
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
        isBlockPerformed = isBlocking;
    }
}
