using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MushroomLightAttackState : EntityState<Mushroom>
{
    private const float stateDuration = 1f;
    private const float activeFrameStart = 0.55f;
    private const float activeFrameEnd = 0.85f;
    private float timer;
    private bool hasDealtDamage = false;
    public MushroomLightAttackState(Mushroom mushroom) : base(mushroom)
    {
    }

    public override void Enter()
    {
        entity.MovementController.StopMovement();
        entity.AnimationController.PlayAnimation(CharacterAnimationState.LightAttack1, entity.MovementController.MovementSpeed);
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

        Vector2 boxSize = entity.CharacterPhysicsController.HitRange.bounds.size;
        Vector2 attackCenter = (Vector2)entity.CharacterPhysicsController.HitRange.bounds.center;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackCenter, boxSize, angle, entity.CharacterCombatController.AttackLayerMask);

        for (int i = 0; i < hitEnemies.Length; i++)
        {
            if (hitEnemies[i].CompareTag("Hitbox"))
            {
                IDamageable damageable = hitEnemies[i].GetComponentInParent<IDamageable>();
                damageable?.TakeDamage(entity.CharacterCombatController.AttackDamage);
            }
        }
    }
}
