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

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        uIEnemyHealth.UpdateHealthBar(HealthSystem.PercentageValue);
    }
}
