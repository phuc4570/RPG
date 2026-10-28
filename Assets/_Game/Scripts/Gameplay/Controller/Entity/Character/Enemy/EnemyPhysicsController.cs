using UnityEngine;

public class EnemyPhysicsController : CharacterPhysicsController
{
    public Collider2D Radar { get; private set; }

    public EnemyPhysicsController(Character entity) : base(entity)
    {

    }

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
