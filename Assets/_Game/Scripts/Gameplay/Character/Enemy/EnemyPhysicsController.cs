using UnityEngine;

public class EnemyPhysicsController : EntityPhysicsController
{
    protected Collider2D Radar { get; private set; }

    public virtual void InitRadar(Collider2D radar)
    {
        Radar = radar;
    }

    public override void ActiveAllColliders(bool flag)
    {
        base.ActiveAllColliders(flag);
        ActiveRadar(flag);
    }

    public virtual void ActiveRadar(bool flag)
    {
        Radar.enabled = flag;
    }
}
