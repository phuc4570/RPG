using UnityEngine;

public class EnemyRadar : EntityAreaTrigger<Enemy>
{
    protected override void TriggerEnter(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            base.TriggerEnter(collision);
            MainCharacter mainCharacter = collision.GetComponentInParent<MainCharacter>();
            if (mainCharacter != null)
            {
                Entity.MovementController.Target = mainCharacter.gameObject;
            }
        }
    }

    protected override void TriggerExit(Collider2D collision)
    {
        if (!collision.isTrigger)
        {
            base.TriggerExit(collision);
            Entity.MovementController.Target = null;
        }
    }
}
