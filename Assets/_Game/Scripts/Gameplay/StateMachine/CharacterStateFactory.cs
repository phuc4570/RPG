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
    Healing             = 10,
    Disappear           = 11,
    Stunned             = 12,
    Freezed             = 13
}

public abstract class CharacterStateFactory<T> : StateFactory<T, CharacterStateType> where T : Character
{
    public CharacterStateFactory(T entity) : base(entity)
    {

    }
}
