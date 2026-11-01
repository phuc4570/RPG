public interface IDamageable
{
    public abstract void TakeDamage(DamageData damageData);
}

public class DamageData
{
    public Entity Source { get; private set; }
    public int Damage { get; private set; }
    public AttackTypeEnum AttackType { get; private set; }
    public float AttackForce { get; private set; }
    public StatusEffectType Type { get; private set; }
    public float Duration { get; private set; }
    public float Value { get; private set; }

    public DamageData(Entity source, int damage, AttackTypeEnum attackType, float attackForce = 0f, StatusEffectType type = StatusEffectType.None, float duration = 0f, float value = 0f)
    {
        Source = source;
        Damage = damage;
        AttackType = attackType;
        AttackForce = attackForce;
        Type = type;
        Duration = duration;
        Value = value;
    }

    public void SetDamage(int newDamage)
    {
        Damage = newDamage;
    }
}