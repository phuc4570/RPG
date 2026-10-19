using UnityEngine;

public class MainCharacterMovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void ApplyMovement(Vector2 moveInput, float moveSpeed)
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void ApplyDodgeImpulse(Vector2 dodgeDirection, float dodgeForce)
    {
        StopMovement();
        rb.AddForce(dodgeDirection * dodgeForce, ForceMode2D.Impulse);
    }
}
