using UnityEngine;

public class Boss : Character
{
    public override void DisableEntity()
    {
        base.DisableEntity();
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        GameEvent.RaiseUpdateBossHealthBar(HealthSystem.PercentageValue);
    }
}
