using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UIViewAnimator;

public abstract class EntityAnimationController : MonoBehaviour
{
    [SerializeField, Required] protected Animator animator;

    protected virtual float BaseMoveSpeed { get; set; }

    public virtual void Initialize<TAnimState>(TAnimState animationState, float animationSpeed) where TAnimState : Enum
    {
        PlayAnimation(animationState, animationSpeed);
    }
    public abstract void PlayAnimation<TAnimState>(TAnimState animationState, float animationSpeed) where TAnimState: Enum;
    public abstract TAnimState GetCurrentAnimation<TAnimState>() where TAnimState : Enum;
    public virtual void UpdateAnimationSpeed(float animationSpeed)
    {

    }
    protected virtual float CalculateSpeedRatio(float animationSpeed)
    {
        return Mathf.Clamp(animationSpeed / Mathf.Max(BaseMoveSpeed, 0.001f), 0.1f, 2.0f);
    }
}

public abstract class EntityAnimationController<T> : EntityAnimationController where T : Enum
{
    private T currentAnimationState;

    public override void PlayAnimation<TAnimState>(TAnimState animationState, float animationSpeed)
    {
        if (typeof(TAnimState) == typeof(T))
        {
            T typedState = Unsafe.As<TAnimState, T>(ref animationState);
            currentAnimationState = typedState;
            PlayAnimation(typedState, animationSpeed);
        }
    }

    public override TAnimState GetCurrentAnimation<TAnimState>()
    {
        if (typeof(TAnimState) == typeof(T))
        {
            return Unsafe.As<T, TAnimState>(ref currentAnimationState);
        }
        return default;
    }

    protected abstract void PlayAnimation(T animationState, float animationSpeed);
}