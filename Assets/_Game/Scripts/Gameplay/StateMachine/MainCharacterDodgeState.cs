using UnityEngine;

public class MainCharacterDodgeState : EntityState<MainCharacter>
{
    private float dodgeDuration = 0.5f;
    private float dodgeTimer = 0f;

    public MainCharacterDodgeState(MainCharacter character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.IsInvincible = true;
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Dodge, entity.MovementSpeed);
        entity.MainCharacterMovementController.ApplyDodgeImpulse(entity.Direction, entity.DodgeForce);
        dodgeTimer = 0f;
        //if (entity.CombatDetector.IsBossAttackingInDodgeWindow())
        //{
        //    TriggerPerfectDodge();
        //}
    }

    public override void Update()
    {
        dodgeTimer += Time.deltaTime;

        if (dodgeTimer >= dodgeDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        entity.IsInvincible = false;
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
}
