using UnityEngine;

public class MainCharacterStateFactory : CharacterStateFactory<MainCharacter>
{
    public MainCharacterStateFactory(MainCharacter entity) : base(entity)
    {

    }

    protected override void RegisterStates()
    {
        RegisterState(CharacterStateType.Idle, new MainCharacterIdleState(entity));
        RegisterState(CharacterStateType.Moving, new MainCharacterMovingState(entity));
        RegisterState(CharacterStateType.Block, new MainCharacterBlockState(entity));
        RegisterState(CharacterStateType.BlockIdle, new MainCharacterBlockIdleState(entity));
        RegisterState(CharacterStateType.Dodge, new MainCharacterDodgeState(entity));
        IsStagesRegistered = true;
    }
}
