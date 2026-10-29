using UnityEngine;

public class MimicStateFactory : CharacterStateFactory<Mimic>
{
    public MimicStateFactory(Mimic mimic) : base(mimic)
    {

    }

    protected override void RegisterStates()
    {
        base.RegisterStates();
        RegisterState(CharacterStateType.Idle, new MimicIdleState(Entity));
        RegisterState(CharacterStateType.Moving, new MimicMovingState(Entity));
        RegisterState(CharacterStateType.LightAttack, new MimicLightAttackState(Entity));
        RegisterState(CharacterStateType.HeavyAttack, new MimicHeavyAttackState(Entity));
        RegisterState(CharacterStateType.Hurt, new MimicHurtState(Entity));
        RegisterState(CharacterStateType.Dead, new MimicDeadState(Entity));
        RegisterState(CharacterStateType.Disappear, new MimicDisappearState(Entity));
        RegisterState(CharacterStateType.Stunned, new MimicStunnedState(Entity));
        RegisterState(CharacterStateType.Transform, new MimicTransformState(Entity));
    }
}
