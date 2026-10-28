using UnityEngine;

public abstract class CharacterCombatController : EntityCombatController<Character>
{
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

    protected CharacterCombatController(Character character) : base(character)
    {

    }

    public virtual void InitAttackLayerMask(LayerMask layerMask)
    {
        AttackLayerMask = layerMask;
    }

    public virtual void InitBaseAttackDamage(int baseAttackDamage)
    {
        BaseAttackDamage = baseAttackDamage;
    }
}
