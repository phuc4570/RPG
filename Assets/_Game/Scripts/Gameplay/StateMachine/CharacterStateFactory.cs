using UnityEngine;

public enum CharacterStateType
{
    Idle                = 0,
    Moving              = 1,
    Block               = 2,
    BlockIdle           = 3,  
    Dodge               = 4,
    LightAttack         = 5,
    HeavyAttackCharge   = 6,
    HeavyAttack         = 7,
    Hurt                = 8,
    Dead                = 9,
}

public class CharacterStateFactory<T> : StateFactory<T, CharacterStateType> where T : Character
{
    public CharacterStateFactory(T entity) : base(entity)
    {

    }

    protected override void RegisterStates()
    {
        RegisterState(CharacterStateType.Idle, new CharacterIdleState<T>(entity));
        RegisterState(CharacterStateType.Moving, new CharacterMovingState<T>(entity));
        RegisterState(CharacterStateType.Hurt, new CharacterHurtState<T>(entity));
        RegisterState(CharacterStateType.Dead, new CharacterDeadState<T>(entity));
        IsStagesRegistered = true;
    }
}
