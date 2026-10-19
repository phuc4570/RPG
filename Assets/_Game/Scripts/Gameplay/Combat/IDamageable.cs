public interface IDamageable
{
    public abstract void TakeDamage(int damage, StatusEffectType type = StatusEffectType.None, float duration = 0f, float value = 0f);
}
