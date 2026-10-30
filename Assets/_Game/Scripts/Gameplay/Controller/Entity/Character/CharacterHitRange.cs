using UnityEngine;

public class CharacterHitRange : EntityAreaTrigger<Character>
{
    [SerializeField] private CharacterAttackTypeEnum type;
    public CharacterAttackTypeEnum Type => type;
    protected override void TriggerEnter(Collider2D collision)
    {
        base.TriggerEnter(collision);
        if (collision.CompareTag("Hitbox"))
        {
            Entity.CharacterCombatController.InHitRange(type, true);
        }
    }

    protected override void TriggerExit(Collider2D collision)
    {
        base.TriggerExit(collision);
        if (collision.CompareTag("Hitbox"))
        {
            Entity.CharacterCombatController.InHitRange(type, false);
        }
    }
}
