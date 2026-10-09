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
        }
    }

    protected virtual void PlayAnimation_Idle()
    {
        animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.IdleHash, 0.05f);
    }

    protected virtual void PlayAnimation_Moving()
    {
        animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.MovingHash, 0.05f);
    }

    protected virtual void PlayAnimation_Block()
    {
        animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.BlockHash, 0.05f);
    }

    protected virtual void PlayAnimation_BlockIdle()
    {
        animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.BlockIdleHash, 0.05f);
    }

    protected virtual void PlayAnimation_Dodge()
    {
        animator.CrossFadeInFixedTime(CharacterAnimationPropertyConfig.DodgeHash, 0.05f);
    }

    #endregion
}
