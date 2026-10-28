public class MainChracterCombatController : CharacterCombatController
{
    public bool IsBlocking { get; set; } = false;
    public float DodgeForce { get; private set; }

    public MainChracterCombatController(Character character) : base(character)
    {

    }

    public void InitDodgeForce(float dodgeForce)
    {
        DodgeForce = dodgeForce;
    }
}
