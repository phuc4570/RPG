using UnityEngine;

public class MainCharacterStateFactory : CharacterStateFactory<MainCharacter>
{
    public MainCharacterStateFactory(MainCharacter entity) : base(entity)
    {

    }

    protected override void RegisterStates()
    {
        RegisterState(CharacterStateType.Idle, new MainCharacterIdleState(Entity));
        RegisterState(CharacterStateType.Moving, new MainCharacterMovingState(Entity));
        RegisterState(CharacterStateType.Block, new MainCharacterBlockState(Entity));
        RegisterState(CharacterStateType.BlockIdle, new MainCharacterBlockIdleState(Entity));
        RegisterState(CharacterStateType.Dodge, new MainCharacterDodgeState(Entity));
        RegisterState(CharacterStateType.LightAttack, new MainCharacterLightAttackState(Entity));
        RegisterState(CharacterStateType.HeavyAttackCharge, new MainCharacterHeavyAttackChargeState(Entity));
        RegisterState(CharacterStateType.HeavyAttack, new MainCharacterHeavyAttackState(Entity));
        RegisterState(CharacterStateType.Hurt, new MainCharacterHurtState(Entity));
        RegisterState(CharacterStateType.Dead, new MainCharacterDeadState(Entity));
        RegisterState(CharacterStateType.Healing, new MainCharacterHealingState(Entity));
        IsStagesRegistered = true;
    }
}
