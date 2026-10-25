using UnityEngine;

public class DummyStateFactory : CharacterStateFactory<Dummy>
{
    public DummyStateFactory(Dummy entity) : base(entity)
    {

    }

    protected override void RegisterStates()
    {
        base.RegisterStates();
        RegisterState(CharacterStateType.Idle, new DummyIdleState(Entity));
        RegisterState(CharacterStateType.Hurt, new DummyHurtState(Entity));
        RegisterState(CharacterStateType.Dead, new DummyDeadState(Entity));
        RegisterState(CharacterStateType.Healing, new DummyHealingState(Entity));
        RegisterState(CharacterStateType.Disappear, new DummyDisappearState(Entity));
        RegisterState(CharacterStateType.Stunned, new DummyStunnedState(Entity));
    }
}
