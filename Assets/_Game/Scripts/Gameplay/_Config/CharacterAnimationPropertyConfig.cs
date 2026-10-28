using UnityEngine;

public class CharacterAnimationPropertyConfig
{
    public static readonly int IdleHash = Animator.StringToHash("Idle");
    public static readonly int MovingHash = Animator.StringToHash("Moving");
    public static readonly int BlockHash = Animator.StringToHash("Block");
    public static readonly int BlockIdleHash = Animator.StringToHash("BlockIdle");
    public static readonly int DodgeHash = Animator.StringToHash("Dodge");
    public static readonly int LightAttack1Hash = Animator.StringToHash("LightAttack1");
    public static readonly int LightAttack2Hash = Animator.StringToHash("LightAttack2");
    public static readonly int LightAttack3Hash = Animator.StringToHash("LightAttack3");
    public static readonly int HeavyAttackHash = Animator.StringToHash("HeavyAttack");
    public static readonly int HurtHash = Animator.StringToHash("Hurt");
    public static readonly int DeadHash = Animator.StringToHash("Dead");
    public static readonly int HealingHash = Animator.StringToHash("Healing");
    public static readonly int DisappearHash = Animator.StringToHash("Disappear");
    public static readonly int StunnedHash = Animator.StringToHash("Stunned");
    public static readonly int TransformHash = Animator.StringToHash("Tranform");
}
