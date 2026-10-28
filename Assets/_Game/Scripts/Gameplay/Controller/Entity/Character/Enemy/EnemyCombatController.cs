using UnityEngine;

public class EnemyCombatController : CharacterCombatController
{
    public bool IsInHitRange { get; set; } = false;

    public EnemyCombatController(Character character) : base(character)
    {

    }
}
