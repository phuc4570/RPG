using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCombatController : EntityCombatController<Character>
{
    private static List<CharacterAttackTypeEnum> AttackTypeCache = new List<CharacterAttackTypeEnum>();
    private Dictionary<CharacterAttackTypeEnum, bool> inHitAttackTypeDict = new Dictionary<CharacterAttackTypeEnum, bool>();

    public LayerMask AttackLayerMask { get; private set; }
    private int attackLayerMaskInLog2 = -1;
    public int AttackLayerMaskInLog2
    {
        get
        {
            if (attackLayerMaskInLog2 == -1)
            {
                attackLayerMaskInLog2 = Mathf.RoundToInt(Mathf.Log(AttackLayerMask.value, 2));
            }
            return attackLayerMaskInLog2;
        }
    }
    public int BaseAttackDamage { get; protected set; }
    public int AttackDamage => BaseAttackDamage;
    public float BaseAttackSpeed { get; protected set; }
    public bool CanAttack { get; protected set; } = true;
    private float attackTimer = 0f;

    protected CharacterCombatController(Character character) : base(character)
    {
        if(AttackTypeCache.Count == 0)
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
            inHitAttackTypeDict[attackType] = false;
        }
    }

    public virtual void InitAttackLayerMask(LayerMask layerMask)
    {
        AttackLayerMask = layerMask;
    }

    public virtual void InitBaseAttackDamage(int baseAttackDamage)
    {
        BaseAttackDamage = baseAttackDamage;
    }

    public virtual void InitBaseAttackSpeed(float baseAttackSpeed)
    {
        BaseAttackSpeed = Mathf.Clamp(baseAttackSpeed, 0.01f, 10);
    }

    public virtual void InHitRange(CharacterAttackTypeEnum type, bool flag)
    {
        inHitAttackTypeDict[type] = flag;
    }

    public bool IsInHitRange(CharacterAttackTypeEnum type) => inHitAttackTypeDict[type];

    public virtual void OnAttackOneHit()
    {
        CanAttack = false;
    }

    public override void Update(float deltaTime)
    {
        if (!CanAttack)
        {
            attackTimer += deltaTime;
            if(attackTimer >= (1.0f / BaseAttackSpeed))
            {
                CanAttack = true;
                attackTimer = 0f;
            }
        }
    }

    public override void Reset()
    {
        CanAttack = false;
        attackTimer = 0f;
        foreach (var attackType in AttackTypeCache)
        {
            inHitAttackTypeDict[attackType] = false;
        }
    }
}
