using UnityEngine;

public class CharacterAnimationPropertyConfig
{
    public static readonly int IdleHash = Animator.StringToHash("Idle");
    public static readonly int MovingHash = Animator.StringToHash("Moving");
    public static readonly int BlockHash = Animator.StringToHash("Block");
    public static readonly int BlockIdleHash = Animator.StringToHash("BlockIdle");
    public static readonly int DodgeHash = Animator.StringToHash("Dodge");
}
