using UnityEngine;

public class MainCharacterHeavyAttackState : EntityState<MainCharacter>
{
    private const float stateDuration = 0.5f;
    private const float activeFrameStart = 0.275f;
    private const float activeFrameEnd = 0.425f;
    private int chargeLevel = 0;
    private float timer;
    private bool hasDealtDamage = false;
    private CharacterStateType nextState = CharacterStateType.Idle;

    private MainCharacterInputReader mainCharacterInputReader;
    public MainCharacterHeavyAttackState(MainCharacter mainCharacter) : base(mainCharacter)
    {
        this.mainCharacterInputReader = mainCharacter.MainCharacterInputReader;
    }

    public override void Enter()
    {
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.HeavyAttack, entity.MovementSpeed);

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

    public void SetChargeLevel(int level)
    {
        chargeLevel = level;
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
        Vector2 dir = Vector2.right;
        if (entity.Direction.x < 0)
        {
            dir = Vector2.left;
        }
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector2 boxSize = new Vector2(1.5f, 0.8f);
        Vector2 attackCenter = (Vector2)entity.transform.position + dir * 0.75f + Vector2.up * 0.25f;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCenter, boxSize, angle, entity.AttackLayerMask);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i].CompareTag("Hitbox"))
            {
                IDamageable damageable = hitEnemies[i].GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(entity.AttackDamage * (chargeLevel + 1));

                entity.FocusSystem.IncreaseFocusFill(0.5f);
            }
        }
    }
}
