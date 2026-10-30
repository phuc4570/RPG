public class EnemyCombatController : CharacterCombatController
{
    public int AttackCount { get; protected set; } = 0;

    public EnemyCombatController(Character character) : base(character)
    {

    }

    public override void OnAttackOneHit()
    {
        base.OnAttackOneHit();
        AttackCount++;
    }

    public virtual void ResetAttackCount()
    {
        AttackCount = 0;
    }

    public override void Reset()
    {
        base.Reset();
        ResetAttackCount();
    }
}
