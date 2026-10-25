using UnityEngine;

public class SuperDummyIdleState : CharacterIdleState<SuperDummy>
{
    public SuperDummyIdleState(SuperDummy superDummy) : base(superDummy)
    {

    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);

        if (entity.MovementController.Target != null)
        {
            entity.Direction = (entity.MovementController.Target.transform.position - entity.transform.position).normalized;
        }
    }
}
