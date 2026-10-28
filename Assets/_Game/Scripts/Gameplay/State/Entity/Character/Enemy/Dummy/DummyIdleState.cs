using UnityEngine;

public class DummyIdleState : CharacterIdleState<Dummy>
{
    public DummyIdleState(Dummy dummy) : base(dummy)
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
