using UnityEngine;

public class Boss : Character
{
    private const float poiseDamageReduction = 0.1f;
    private const float poiseStunDuration = 3f;
    public BossPoiseSystem PoiseSystem { get; private set; }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new BarStatSystem<Boss>(this);
        PoiseSystem = new BossPoiseSystem(this);
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(1000);
        PoiseSystem.Init(50);
        PoiseSystem.SetDuration(poiseStunDuration);
    }

    public override void DisableEntity()
    {
        base.DisableEntity();
    }

    protected override void OnTakeDamage(ref int damage)
    {
        base.OnTakeDamage(ref damage);
        if (PoiseSystem.Value > 0)
        {
            PoiseSystem.AddValue(-5);
            damage = Mathf.RoundToInt(damage * poiseDamageReduction);
            if (damage < 1) damage = 1;
            if(PoiseSystem.PercentageValue == 0)
            {
                StatusEffectManager.AddStatusEffect(StatusEffectType.Stun, poiseStunDuration);
            }
        }
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        GameEvent.RaiseUpdateBossHealthBar(HealthSystem.PercentageValue);
    }
}
