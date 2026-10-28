using UnityEngine;

public class DummyDeadState : CharacterDeadState<Dummy>
{
    protected override float StateDuration => 0.25f;
    public DummyDeadState(Dummy dummy) : base(dummy)
    {

    }

    public override void Update(float deltaTime)
    {
        stateTimer += deltaTime;
        if (stateTimer >= StateDuration + delayDuration)
        {
            entity.StateMachine.ChangeState(entity.StateFactory.GetState(CharacterStateType.Healing));
        }
    }
}
