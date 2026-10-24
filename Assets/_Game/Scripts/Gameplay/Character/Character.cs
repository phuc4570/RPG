using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity
{
    [SerializeField] private LayerMask attackLayerMask;

    public LayerMask AttackLayerMask => attackLayerMask;
    private int attackLayerMaskInLog2 = -1;
    public int AttackLayerMaskInLog2
    {
        get
        {
            if(attackLayerMaskInLog2 == -1)
            {
                attackLayerMaskInLog2 = Mathf.RoundToInt(Mathf.Log(attackLayerMask.value, 2));
            }
            return attackLayerMaskInLog2;
        }
    }

    public int BaseAttackDamage { get; protected set; }
    public int AttackDamage => BaseAttackDamage;

    protected override void SetUpAnimationController()
    {
        AnimationController = new CharacterAnimationController();
    }

    protected override void SetUpPhysicsController()
    {
        PhysicsController = new CharacterPhysicsController(this);
    }

    protected override void SetUpMovementController()
    {
        MovementController = new CharacterMovementController<Character>(this);
    }

    protected override void InitStateMachine()
    {
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    protected override void OnTakeDamage(ref int damage)
    {
        base.OnTakeDamage(ref damage);

        if (HealthSystem.Value <= 0)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Dead));
            return;
        }

        StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Hurt));
    }

    protected override void OnHandleStatusEffect(StatusEffectType type, bool isAdded)
    {
        base.OnHandleStatusEffect(type, isAdded);
        if (StatusEffectManager.HasStatusEffect(StatusEffectType.Freezed))
        {
            //Handle Character Visual Effect
        }
        if (StatusEffectManager.HasStatusEffect(StatusEffectType.Stunned))
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Stunned));
            return;
        }
        switch (type)
        {
            case StatusEffectType.Stunned:
            case StatusEffectType.Freezed:
                if(isAdded == false)
                {
                    StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Idle));
                }
                break;
        }
    }
}
