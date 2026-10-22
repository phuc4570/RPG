using UnityEngine;

public class SuperDummyStateFactory : CharacterStateFactory<SuperDummy>
{
    public SuperDummyStateFactory(SuperDummy entity) : base(entity)
    {

    }

    protected override void RegisterStates()
    {
        base.RegisterStates();
        RegisterState(CharacterStateType.Idle, new SuperDummyIdleState(Entity));
        RegisterState(CharacterStateType.Hurt, new SuperDummyHurtState(Entity));
        RegisterState(CharacterStateType.Dead, new SuperDummyDeadState(Entity));
        RegisterState(CharacterStateType.Healing, new SuperDummyHealingState(Entity));
        RegisterState(CharacterStateType.Disappear, new SuperDummyDisappearState(Entity));
    }
}
