using UnityEngine;

public class MushroomStateFactory : CharacterStateFactory<Mushroom>
{
    public MushroomStateFactory(Mushroom mushroom) : base(mushroom)
    {

    }

    protected override void RegisterStates()
    {
        base.RegisterStates();
        RegisterState(CharacterStateType.Idle, new MushroomIdleState(Entity));
        RegisterState(CharacterStateType.Moving, new MushroomMovingState(Entity));
        RegisterState(CharacterStateType.LightAttack, new MushroomLightAttackState(Entity));
        RegisterState(CharacterStateType.HeavyAttack, new MushroomHeavyAttackState(Entity));
        RegisterState(CharacterStateType.Hurt, new MushroomHurtState(Entity));
        RegisterState(CharacterStateType.Dead, new MushroomDeadState(Entity));
        RegisterState(CharacterStateType.Disappear, new MushroomDisappearState(Entity));
        RegisterState(CharacterStateType.Stunned, new MushroomStunnedState(Entity));
    }
}
