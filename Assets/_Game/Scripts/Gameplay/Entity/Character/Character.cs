using UnityEngine;

public abstract class Character : Entity
{
    [SerializeField] private LayerMask attackLayerMask;
    [SerializeField] private Collider2D hitRangeCollider;
    public CharacterCombatController CharacterCombatController => CombatController as CharacterCombatController;
    public CharacterPhysicsController CharacterPhysicsController => PhysicsController as CharacterPhysicsController;

    public override void EnableEntity()
    {
        base.EnableEntity();
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

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

    protected override void InitCombatController()
    {
        CharacterCombatController.InitAttackLayerMask(attackLayerMask);
    }

    protected override void InitPhysicsController()
    {
        base.InitPhysicsController();
        CharacterPhysicsController.InitHitRange(hitRangeCollider);
    }

    protected override void InitStateMachine()
    {
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    protected override void AfterTakeDamage()
    {
        base.AfterTakeDamage();
        if (!IsAlive)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Dead));
            return;
        }

        if (StatusEffectManager.CanBeInterupt)
        {
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Hurt));
        }
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
