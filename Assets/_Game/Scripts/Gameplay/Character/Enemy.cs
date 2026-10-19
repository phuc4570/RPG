using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private UIEnemyHealth uIEnemyHealth;

    public override void DisableEntity()
    {

    }

    public override void TakeDamage(int damage, StatusEffectType type = StatusEffectType.None, float duration = 0, float value = 0)
    {
        base.TakeDamage(damage, type, duration, value);
    }

    public override void OnHealthValueUpdated()
    {
        base.OnHealthValueUpdated();
        uIEnemyHealth.UpdateHealthBar(HealthSystem.PercentageValue);
    }
}
