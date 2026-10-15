using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new CharacterStateFactory<Character>(this);
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new MainCharacterHealthSystem(100);
    }

    public override void SetUpProperty()
    {
        BaseMovementSpeed = 2f;
        Direction = Vector2.left;
        SetUpBarSystem();
    }

    public override void SetUpStatusEffect()
    {
        StatusEffectManager.ClearStatusEffects();
    }   

    public override void SetUpAnimation()
    {
        AnimationController.Initialize(CharacterAnimationState.Idle, MovementSpeed);
    }
}
