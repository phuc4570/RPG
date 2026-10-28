using UnityEngine;

public class EnemyRadar : EntityAreaTrigger<Enemy>
{
    protected override void TriggerEnter(Collider2D collision)
    {
        base.TriggerEnter(collision);
        Entity.MovementController.Target = collision.GetComponentInParent<MainCharacter>().gameObject;
    }

    protected override void TriggerExit(Collider2D collision)
    {
        base.TriggerExit(collision);
        Entity.MovementController.Target = null;
    }
}
