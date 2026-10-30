using DG.DemiLib;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPhysicsController : EntityPhysicsController<Character>
{
    private static List<CharacterAttackTypeEnum> AttackTypeCache = new List<CharacterAttackTypeEnum>();
    private Dictionary<CharacterAttackTypeEnum, Collider2D> hitRangeAttackTypeDict = new Dictionary<CharacterAttackTypeEnum, Collider2D>();

    private List<Collider2D> hitRanges = new List<Collider2D>();
    public IReadOnlyList<Collider2D> HitRanges => hitRanges;

    public CharacterPhysicsController(Character entity) : base(entity)
    {
        if (AttackTypeCache.Count == 0)
        {
            var stringEnums = Enum.GetNames(typeof(CharacterAttackTypeEnum));
            foreach (var stringEnum in stringEnums)
            {
                if (Enum.TryParse<CharacterAttackTypeEnum>(stringEnum, out var result))
                {
                    AttackTypeCache.Add(result);
                }
            }
        }

        foreach (var attackType in AttackTypeCache)
        {
            hitRangeAttackTypeDict[attackType] = null;
        }
    }

    public virtual void InitHitRanges(List<CharacterHitRange> hitRanges)
    {
        for (int i = 0; i < hitRanges.Count; i++)
        {
            this.hitRanges.Add(hitRanges[i].Area);
            hitRangeAttackTypeDict[hitRanges[i].Type] = hitRanges[i].Area;
        }
    }

    public override void ActiveAllColliders(bool flag)
    {
        base.ActiveAllColliders(flag);
        ActiveHitRanges(flag);
    }

    public virtual void ActiveHitRanges(bool flag)
    {
        foreach(var hitRange in hitRanges)
        {
            hitRange.enabled = flag;
        }
    }

    public virtual Collider2D GetHitRange(CharacterAttackTypeEnum attackType)
    {
        return hitRangeAttackTypeDict[attackType];
    }
}
