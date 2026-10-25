using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyTypeEnum type;
    [SerializeField] private Collider2D radar;
    [SerializeField] private UIEnemyHealth uIEnemyHealth;
    public EnemyPhysicsController EnemyPhysicsController => PhysicsController as EnemyPhysicsController;
    public EnemyConfigData Data { get; protected set; }

    protected override void SetUpPhysicsController()
    {
        PhysicsController = new EnemyPhysicsController();
    }
    protected override void SetUpProperty()
    {
        base.SetUpProperty();
        uIEnemyHealth.gameObject.SetActive(true);
    }

    protected override void Init()
    {
        Data = EnemyConfig.Ins.GetData(type);
        base.Init();
    }

    protected override void InitPhysicsController()
    {
        base.InitPhysicsController();
        EnemyPhysicsController.InitRadar(radar);
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

    protected override void InitProperty()
    {
        base.InitProperty();
        if (Data != null)
        {
            BaseMovementSpeed = Data.BaseMovementSpeed;
            BaseAttackDamage = Data.BaseAttackDamage;
        }
    }

    public override void PreDisableEntity()
    {
        base.PreDisableEntity();
        uIEnemyHealth.gameObject.SetActive(false);
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
