using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Collider2D radar;
    [SerializeField] private UIEnemyHealth uIEnemyHealth;
    public EnemyPhysicsController EnemyPhysicsController => PhysicsController as EnemyPhysicsController;

    protected override void SetUpPhysicsController()
    {
        PhysicsController = new EnemyPhysicsController();
    }
    protected override void SetUpProperty()
    {
        base.SetUpProperty();
        uIEnemyHealth.gameObject.SetActive(true);
    }

    protected override void InitPhysicsController()
    {
        base.InitPhysicsController();
        EnemyPhysicsController.InitRadar(radar);
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(100);
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

    protected override void OnTakeDamage(ref int damage)
    {
        base.OnTakeDamage(ref damage);
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        uIEnemyHealth.UpdateHealthBar(HealthSystem.PercentageValue);
    }
}
