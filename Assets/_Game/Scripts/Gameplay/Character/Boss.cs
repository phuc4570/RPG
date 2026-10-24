using UnityEngine;

public class Boss : Character
{
    [SerializeField] private BossTypeEnum type;
    private const float poiseDamageReduction = 0.1f;
    private const float poiseStunDuration = 3f;
    public BossPoiseSystem PoiseSystem { get; private set; }
    public BossConfigData Data { get; protected set; }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new BarStatSystem<Boss>(this);
        PoiseSystem = new BossPoiseSystem(this);
    }

    protected override void Init()
    {
        Data = BossConfig.Ins.GetData(type);
        base.Init();
    }

    protected override void InitBarSystem()
    {
        if (Data != null)
        {
            HealthSystem.Init(Data.Health);
            PoiseSystem.Init(50);
            PoiseSystem.SetDuration(poiseStunDuration);
        }
        else
        {
            base.InitBarSystem();
        }
    }

    protected override void InitProperty()
    {
        if (Data != null)
        {
            BaseMovementSpeed = Data.BaseMovementSpeed;
            BaseAttackDamage = Data.BaseAttackDamage;
        }
        else
        {
            base.InitProperty();
        }
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
                StatusEffectManager.AddStatusEffect(StatusEffectType.Stunned, poiseStunDuration);
            }
        }
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        GameEvent.RaiseUpdateBossHealthBar(HealthSystem.PercentageValue);
    }
}
