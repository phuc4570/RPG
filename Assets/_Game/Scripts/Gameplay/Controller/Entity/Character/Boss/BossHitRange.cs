using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

public class BossHitRange : EntityAreaTrigger<Boss>
{
    [SerializeField] private BossAttackType type;
    protected override void TriggerEnter(Collider2D collision)
    {
        base.TriggerEnter(collision);
        if (collision.CompareTag("Hitbox"))
        {
            TriggerHit(true);
        }
    }

    protected override void TriggerExit(Collider2D collision)
    {
        base.TriggerExit(collision);
        if (collision.CompareTag("Hitbox"))
        {
            TriggerHit(false);
        }
    }

    protected virtual void TriggerHit(bool flag)
    {
        switch (type)
        {
            case BossAttackType.LightAttack:
                Entity.BossCombatController.InHitRange(type, flag);
                break;
            case BossAttackType.HeavyAttack:
                Entity.BossCombatController.InHitRange(type, flag);
                break;
        }
    }
}
