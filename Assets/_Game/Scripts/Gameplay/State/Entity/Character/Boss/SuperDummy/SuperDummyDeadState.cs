using UnityEngine;

public class SuperDummyDeadState : CharacterDeadState<SuperDummy>
{
    protected override float StateDuration => 0.25f;
    public SuperDummyDeadState(SuperDummy superDummy) : base(superDummy)
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
