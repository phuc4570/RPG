using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : EntityAnimationController<CharacterAnimationState>
{
    #region Play Animation

    protected override void PlayAnimation(CharacterAnimationState animationState, float animationSpeed)
    {
        switch (animationState)
        {
            case CharacterAnimationState.Idle:
                PlayAnimation_Idle();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Moving:
                PlayAnimation_Moving();
                UpdateAnimationSpeed(animationSpeed);
                break;
            case CharacterAnimationState.Block:
                PlayAnimation_Block();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.BlockIdle:
                PlayAnimation_BlockIdle();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Dodge:
                PlayAnimation_Dodge();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.LightAttack1:
                PlayAnimation_Attack1();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.LightAttack2:
                PlayAnimation_Attack2();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.LightAttack3:
                PlayAnimation_Attack3();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.HeavyAttack:
                PlayAnimation_HeavyAttack();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Hurt:
                PlayAnimation_Hurt();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Dead:
                PlayAnimation_Dead();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Healing:
                PlayAnimation_Heal();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Disappear:
                PlayAnimation_Disappear();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
            case CharacterAnimationState.Stunned:
                PlayAnimation_Stunned();
                UpdateAnimationSpeed(BaseMoveSpeed);
                break;
        }
    }

    protected virtual void PlayAnimation_Idle()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.IdleHash, 0.05f);
    }

    protected virtual void PlayAnimation_Moving()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.MovingHash, 0.05f);
    }

    protected virtual void PlayAnimation_Block()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.BlockHash, 0.05f);
    }

    protected virtual void PlayAnimation_BlockIdle()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.BlockIdleHash, 0.05f);
    }

    protected virtual void PlayAnimation_Dodge()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.DodgeHash, 0.05f);
    }

    protected virtual void PlayAnimation_Attack1()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.LightAttack1Hash, 0.05f);
    }

    protected virtual void PlayAnimation_Attack2()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.LightAttack2Hash, 0.05f);
    }

    protected virtual void PlayAnimation_Attack3()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.LightAttack3Hash, 0.05f);
    }

    protected virtual void PlayAnimation_HeavyAttack()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.HeavyAttackHash, 0.05f);
    }

    protected virtual void PlayAnimation_Hurt()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.HurtHash, 0.05f);
    }

    protected virtual void PlayAnimation_Dead()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.DeadHash, 0.05f);
    }

    protected virtual void PlayAnimation_Heal()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.HealingHash, 0.05f);
    }

    protected virtual void PlayAnimation_Disappear()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.DisappearHash, 0.05f);
    }

    protected virtual void PlayAnimation_Stunned()
    {
        Animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.StunnedHash, 0.05f);
    }

    #endregion
}
