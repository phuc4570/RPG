using DG.DemiLib;
using UnityEngine;

public class EnemyCombatController : CharacterCombatController
{
    public bool IsInHitRange { get; protected set; } = false;
    public int AttackCount { get; protected set; } = 0;

    public EnemyCombatController(Character character) : base(character)
    {

    }

    public virtual void InHitRange(bool flag)
    {
        IsInHitRange = flag;
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
        InHitRange(false);
        ResetAttackCount();
    }
}
