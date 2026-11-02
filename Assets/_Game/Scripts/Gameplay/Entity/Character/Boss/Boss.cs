using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    [SerializeField] private BossTypeEnum type;
    public BossCombatController BossCombatController => CombatController as BossCombatController;
    public BossPoiseSystem PoiseSystem { get; private set; }
    public BossConfigData Data { get; protected set; }

    public override void EnableEntity()
    {
        base.EnableEntity();
        PoiseSystem.AddValue(PoiseSystem.MaxValue);
        StatusEffectManager.AddStatusEffect(StatusEffectType.Hype, 999999999);
    }

    protected override void SetUpCombatController()
    {
        CombatController = new BossCombatController(this);
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
            CharacterCombatController.InitBaseAttackSpeed(Data.BaseAttackSpeed);
            CharacterCombatController.InitAttackForce(Data.AttackForce);
            BossCombatController.InitPoiseDamageReduction(0.1f);
            BossCombatController.InitPoiseStunDuration(3f);
            BossCombatController.InitPoiseAttackLost(5);
            BossCombatController.InitPoiseParryLost(15);
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
        GameManager.Ins.OnWin();
    }

    protected override void BeforeTakeDamage(DamageData damageData)
    {
        base.BeforeTakeDamage(damageData);
        if (PoiseSystem.Value > 0)
        {
            PoiseSystem.AddValue(-5);
            damageData.SetDamage(Mathf.Clamp(Mathf.RoundToInt(damageData.Damage * BossCombatController.PoiseDamageReduction), 1, int.MaxValue));
        }
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        GameEvent.RaiseUpdateBossHealthBar(HealthSystem.PercentageValue);
    }

    public virtual void OnPoiseValueUpdated()
    {
        if (PoiseSystem.PercentageValue == 0)
        {
            StatusEffectManager.AddStatusEffect(StatusEffectType.Stunned, BossCombatController.PoiseStunDuration);
        }
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