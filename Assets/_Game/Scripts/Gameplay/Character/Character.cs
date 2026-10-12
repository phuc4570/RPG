using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

public abstract class Character : Entity
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new CharacterStateFactory<Character>(this);
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    public override void SetUpProperty()
    {
        
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
