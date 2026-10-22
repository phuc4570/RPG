using UnityEngine;

public class SuperDummyHealingState : CharacterHealingState<SuperDummy>
{
    protected override float StateDuration => 2f;
    private const float healingInterval = 0.1f;
    private int healingStep;
    private float healingTimer;
    public SuperDummyHealingState(SuperDummy superDummy) : base(superDummy)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, StateDuration);
        healingStep = (int)(entity.HealthSystem.MaxValue / (StateDuration / healingInterval));
        healingTimer = 0f;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        healingTimer += deltaTime;
        if(healingTimer >= healingInterval)
        {
            healingTimer -= healingInterval;
            entity.HealthSystem.AddValue(healingStep);
        }
    }

    public override void Exit()
    {
        base.Exit();
        entity.HealthSystem.AddValue(entity.HealthSystem.MaxValue);
        entity.PhysicsController.ActiveAllColliders(true);
    }
}