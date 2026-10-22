using UnityEngine;

public class CharacterMovementController<T> : EntityMovementController<T> where T : Entity
{
    public CharacterMovementController(T entity) : base(entity)
    {

    }
}
