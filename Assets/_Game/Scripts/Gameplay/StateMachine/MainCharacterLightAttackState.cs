using UnityEngine;

public class MainCharacterLightAttackState : EntityState<MainCharacter>
{
    private const float comboResetTime = 0.5f;
    private const float stateDuration = 0.4f;
    private const int staminaCost = 1;
    private const float activeFrameStart = 0.1f;
    private const float activeFrameEnd = 0.25f;

    private int comboIdx = 0;
    private float lastAttackTime;
    private float timer;
    private bool hasDealtDamage = false;
    private CharacterStateType nextState = CharacterStateType.Idle;

    private MainCharacterInputReader mainCharacterInputReader;

    public MainCharacterLightAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
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

        if (lastAttackTime + comboResetTime < Time.time)
        {
            comboIdx = 0;
        }

        entity.MainCharacterMovementController.StopMovement();
        entity.FocusSystem.IncreaseFocusFill(0.2f);
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

        timer = 0f;
        nextState = CharacterStateType.Idle;
        hasDealtDamage = false;

        GameEvent.RaiseVisualAttackFillAnim(stateDuration);

        mainCharacterInputReader.OnBlockPerformed += HandleBlockPerformed;
        mainCharacterInputReader.OnDodgePerformed += HandleDodgePerformed;
        mainCharacterInputReader.OnLightAttackPerformed += HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged += HandleHeavyAttackCharged;
        mainCharacterInputReader.OnHeavyAttackPerformed += HandleHeavyAttackPerformed;
    }

    public override void Update(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= activeFrameStart && timer <= activeFrameEnd && !hasDealtDamage)
        {
            CheckMeleeHit();
            hasDealtDamage = true;
        }

        if (timer >= stateDuration)
        {
            lastAttackTime = Time.time;
            comboIdx = (comboIdx + 1) % 3;

            IState nextStateInterface;
            switch (nextState)
            {
                case CharacterStateType.Dodge:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.Dodge);
                    break;
                case CharacterStateType.BlockIdle:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.BlockIdle);
                    break;
                case CharacterStateType.LightAttack:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.LightAttack);
                    break;
                case CharacterStateType.HeavyAttackCharge:
                    nextStateInterface = entity.StateFactory.GetState(CharacterStateType.HeavyAttackCharge);
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
        mainCharacterInputReader.OnLightAttackPerformed -= HandleLightAttackPerformed;
        mainCharacterInputReader.OnHeavyAttackCharged -= HandleHeavyAttackCharged;
        mainCharacterInputReader.OnHeavyAttackPerformed -= HandleHeavyAttackPerformed;
    }

    private void HandleBlockPerformed(bool isBlocking)
    {
        nextState = isBlocking ? CharacterStateType.BlockIdle : CharacterStateType.Idle;
    }

    private void HandleDodgePerformed()
    {
        nextState = CharacterStateType.Dodge;
    }

    private void HandleLightAttackPerformed()
    {
        nextState = CharacterStateType.LightAttack;
    }
    private void HandleHeavyAttackCharged()
    {
        nextState = CharacterStateType.HeavyAttackCharge;
    }

    private void HandleHeavyAttackPerformed()
    {
        nextState = CharacterStateType.Idle;
    }

    private void CheckMeleeHit()
    {
        Vector2 dir = entity.Direction;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector2 boxSize = new Vector2(1.5f, 0.8f);
        Vector2 attackCenter = (Vector2)entity.transform.position + dir * 0.75f + Vector2.up * 0.25f;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCenter, boxSize, angle, entity.AttackLayerMask);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i].CompareTag("Hitbox"))
            {
                IDamageable damageable = hitEnemies[i].GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(entity.AttackDamage);
            }
        }
    }
}
