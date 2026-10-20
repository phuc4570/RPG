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

    protected override void InitStateMachine()
    {
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();

        if (HealthSystem.Value <= 0)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Dead));
            return;
        }

        StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Hurt));
    }
}
