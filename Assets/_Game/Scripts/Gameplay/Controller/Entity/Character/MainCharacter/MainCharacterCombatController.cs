public class MainCharacterCombatController : CharacterCombatController
{
    public bool IsBlocking { get; set; } = false;
    public bool IsPerfectBlock { get; set; } = false;
    public float DodgeForce { get; private set; }
    public int DodgeStaminaCost { get; private set; }
    public bool IsPerfectDodge { get; set; } = false;
    public float BlockDamageReduction { get; private set; }
    public int BlockStaminaCost { get; private set; }

    public MainCharacterCombatController(Character character) : base(character)
    {

    }

    public void InitDodgeForce(float dodgeForce)
    {
        DodgeForce = dodgeForce;
    }

    public void InitDodgeStaminaCost(int dodgeStaminaCost)
    {
        DodgeStaminaCost = dodgeStaminaCost;
    }

    public void InitBlockDamageReduction(float blockDamageReduction)
    {
        BlockDamageReduction = blockDamageReduction;
    }

    public void InitBlockStaminaCost(int blockStaminaCost)
    {
        BlockStaminaCost = blockStaminaCost;
    }

    public override void Reset()
    {
        base.Reset();
        IsBlocking = false;
    }
}
