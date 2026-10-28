using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Boss : Character
{
    [SerializeField] private BossTypeEnum type;
    private const float poiseDamageReduction = 0.1f;
    private const float poiseStunDuration = 3f;
    public BossPoiseSystem PoiseSystem { get; private set; }
    public BossConfigData Data { get; protected set; }

    public override void EnableEntity()
    {
        base.EnableEntity();
        PoiseSystem.AddValue(PoiseSystem.MaxValue);
    }

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

    protected override void InitMovementController()
    {
        base.InitMovementController();
        if (Data != null)
        {
            MovementController.InitBaseMovementSpeed(Data.BaseMovementSpeed);
        }
    }

    protected override void InitCombatController()
    {
        base.InitCombatController();
        if (Data != null)
        {
            CharacterCombatController.InitBaseAttackDamage(Data.BaseAttackDamage);
        }
    }

    protected override void InitBarSystem()
    {
        if (Data != null)
        {
            HealthSystem.Init(Data.Health);
            PoiseSystem.Init(50);
        }
        else
        {
            base.InitBarSystem();
        }
    }

    public override void DisableEntity()
    {
        base.DisableEntity();
    }

    protected override void BeforeTakeDamage(ref int damage)
    {
        base.BeforeTakeDamage(ref damage);
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

    protected override void OnHandleStatusEffect(StatusEffectType type, bool isAdded)
    {
        base.OnHandleStatusEffect(type, isAdded);
        if (type == StatusEffectType.Stunned && isAdded == false)
        {
            PoiseSystem.AddValue(PoiseSystem.MaxValue);
        }
    }
}