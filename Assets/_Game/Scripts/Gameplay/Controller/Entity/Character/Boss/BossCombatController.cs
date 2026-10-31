public class BossCombatController : CharacterCombatController
{
    public int AttackCount { get; private set; } = 0;
    public float PoiseDamageReduction { get; private set; }
    public int PoiseAttackLost { get; private set; }
    public int PoiseParryLost { get; private set; }
    public float PoiseStunDuration { get; private set; }

    public BossCombatController(Character character) : base(character)
    {
        
    }

    public virtual void InitPoiseDamageReduction(float poiseDamageReduction)
    {
        PoiseDamageReduction = poiseDamageReduction;
    }

    public virtual void InitPoiseAttackLost(int poiseAttackLost)
    {
        PoiseAttackLost = poiseAttackLost;
    }

    public virtual void InitPoiseParryLost(int poiseParryLost)
    {
        PoiseParryLost = poiseParryLost;
    }

    public virtual void InitPoiseStunDuration(float poiseStunDuration)
    {
        PoiseStunDuration = poiseStunDuration;
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
