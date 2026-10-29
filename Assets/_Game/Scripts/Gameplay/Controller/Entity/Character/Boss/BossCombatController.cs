using System;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatController : CharacterCombatController
{
    private static List<BossAttackType> AttackTypeCache = new List<BossAttackType>();
    private Dictionary<BossAttackType, bool> inHitAttackTypeDict = new Dictionary<BossAttackType, bool>();
    public bool IsInHitRange(BossAttackType type) => inHitAttackTypeDict[type];
    public int AttackCount { get; protected set; } = 0;

    public BossCombatController(Character character) : base(character)
    {
        var stringEnums = Enum.GetNames(typeof(BossAttackType));
        foreach (var stringEnum in stringEnums)
        {
            if (Enum.TryParse<BossAttackType>(stringEnum, out var result))
            {
                AttackTypeCache.Add(result);
            }
        }

        foreach (var attackType in AttackTypeCache)
        {
            inHitAttackTypeDict[attackType] = false;
        }
    }

    public virtual void InHitRange(BossAttackType type, bool flag)
    {
        inHitAttackTypeDict[type] = flag;
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
        foreach (var attackType in AttackTypeCache)
        {
            inHitAttackTypeDict[attackType] = false;
        }
    }
}
