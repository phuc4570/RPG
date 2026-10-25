using UnityEngine;

public class MainCharacterMovementController : CharacterMovementController<MainCharacter>
{
    public MainCharacterMovementController(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public void ApplyDodgeImpulse(Vector2 dodgeDirection, float dodgeForce)
    {
        StopMovement();
        Rb.AddForce(dodgeDirection * dodgeForce, ForceMode2D.Impulse);
    }
}
