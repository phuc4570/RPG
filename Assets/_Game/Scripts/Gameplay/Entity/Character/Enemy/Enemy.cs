using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyTypeEnum type;
    [SerializeField] private EnemyRadar radarScript;
    [SerializeField] private UIEnemyHealth uIEnemyHealth;
    public EnemyPhysicsController EnemyPhysicsController => PhysicsController as EnemyPhysicsController;
    public EnemyCombatController EnemyCombatController => CombatController as EnemyCombatController;
    public EnemyConfigData Data { get; protected set; }
    public bool IsAlpha { get; protected set; }

    public void EnableEntity(bool isAlpha)
    {
        IsAlpha = isAlpha;
        EnableEntity();
    }

    public override void EnableEntity()
    {
        base.EnableEntity();
        uIEnemyHealth.gameObject.SetActive(true);
    }

    protected override void SetUpCombatController()
    {
        CombatController = new EnemyCombatController(this);
    }

    protected override void SetUpPhysicsController()
    {
        PhysicsController = new EnemyPhysicsController(this);
    }

    protected override void Init()
    {
        Data = EnemyConfig.Ins.GetData(type);
        base.Init();
    }

    protected override void InitPhysicsController()
    {
        base.InitPhysicsController();
        EnemyPhysicsController.InitRadar(radarScript.Area);
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
            CharacterCombatController.InitBaseAttackSpeed(Data.BaseAttackSpeed);
        }
        radarScript.Init(this);
    }

    protected override void InitBarSystem()
    {
        if(Data != null)
        {
            HealthSystem.Init(Data.Health);
        }
        else
        {
            base.InitBarSystem();
        }
    }

    protected override void InitStatusEffect()
    {
        base.InitStatusEffect();
        if (IsAlpha)
        {
            StatusEffectManager.AddStatusEffect(StatusEffectType.Unstoppable, 999999999);
        }
    }

    public override void DisableEntity()
    {
        base.DisableEntity();
        uIEnemyHealth.gameObject.SetActive(false);
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        uIEnemyHealth.UpdateHealthBar(HealthSystem.PercentageValue);
    }
}
