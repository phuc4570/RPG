using UnityEngine;

public class CharacterPhysicsController : EntityPhysicsController<Character>
{
    public Collider2D HitRange { get; private set; }

    public CharacterPhysicsController(Character entity) : base(entity)
    {

    }

    public virtual void InitHitRange(Collider2D hitRange)
    {
        HitRange = hitRange;
    }

    public override void ActiveAllColliders(bool flag)
    {
        base.ActiveAllColliders(flag);
        ActiveHitRange(flag);
    }

    public virtual void ActiveHitRange(bool flag)
    {
        HitRange.enabled = flag;
    }
}
