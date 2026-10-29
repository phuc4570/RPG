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
    public float BaseAttackSpeed { get; protected set; }
    public bool CanAttack { get; protected set; } = true;
    private float attackTimer = 0f;

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

    public virtual void InitBaseAttackSpeed(float baseAttackSpeed)
    {
        BaseAttackSpeed = Mathf.Clamp(baseAttackSpeed, 0.01f, 10);
    }

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
    }
}
