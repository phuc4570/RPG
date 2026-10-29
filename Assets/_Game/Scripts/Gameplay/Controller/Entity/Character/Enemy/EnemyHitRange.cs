using UnityEngine;

public class EnemyHitRange : EntityAreaTrigger<Enemy>
{
    protected override void TriggerEnter(Collider2D collision)
    {
        base.TriggerEnter(collision);
        if (collision.CompareTag("Hitbox"))
        {
            Entity.EnemyCombatController.InHitRange(true);
        }
    }

    protected override void TriggerExit(Collider2D collision)
    {
        base.TriggerExit(collision);
        if (collision.CompareTag("Hitbox"))
        {
            Entity.EnemyCombatController.InHitRange(false);
        }
    }
}
