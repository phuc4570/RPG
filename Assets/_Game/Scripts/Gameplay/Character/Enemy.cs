using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private UIEnemyHealth uIEnemyHealth;

    public override void DisableEntity()
    {

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
