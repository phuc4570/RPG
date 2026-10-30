using UnityEngine;

public class MimicHeavyAttackState : EntityState<Mimic>
{
    private const float stateDuration = 1f;
    private const float activeFrameStart = 0.66f;
    private const float activeFrameEnd = 0.83f;
    private float timer;
    private bool hasDealtDamage = false;

    public MimicHeavyAttackState(Mimic mimic) : base(mimic)
    {

    }

    public override bool ValidateCondition()
    {
        return entity.CharacterCombatController.CanAttack;
    }

    public override void Enter()
    {
        entity.BossCombatController.ResetAttackCount();
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.HeavyAttack, entity.MovementController.MovementSpeed);
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Unstoppable, stateDuration);
        timer = 0f;
        hasDealtDamage = false;
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
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Idle));
        }
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {

    }

    public override void Exit()
    {

    }

    private void CheckMeleeHit()
    {
        Vector2 dir = Vector2.right;
        if (entity.Direction.x < 0)
        {
            dir = Vector2.left;
        }
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Collider2D hitRange = entity.CharacterPhysicsController.GetHitRange(CharacterAttackTypeEnum.HeavyAttack);
        Vector2 boxSize = hitRange.bounds.size;
        Vector2 attackCenter = hitRange.bounds.center;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCenter, boxSize, angle, entity.CharacterCombatController.AttackLayerMask);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i].CompareTag("Hitbox"))
            {
                IDamageable damageable = hitEnemies[i].GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(entity.CharacterCombatController.AttackDamage * 2);
            }
        }
    }
}