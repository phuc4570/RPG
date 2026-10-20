using UnityEngine;

public class Boss : Character
{
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
        //uIEnemyHealth.UpdateHealthBar(HealthSystem.PercentageValue);
    }
}
