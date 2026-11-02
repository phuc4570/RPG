using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Entity : MonoBehaviour, ITickable, IDamageable
{
    #region References
    [SerializeField] private bool defaultSpriteDirectionIsRight = true;
    [SerializeField, Required] private SpriteRenderer rendererObject;
    [SerializeField, Required] private UIStatusEffectList uIStatusEffectList;
    [SerializeField, Required] private Animator animator;
    [SerializeField, Required] private Collider2D mainCollider;
    [SerializeField, Required] private Collider2D hitBox;
    [SerializeField, Required] private Rigidbody2D rb;
    #endregion

    #region Public Properties
    public SpriteRenderer RendererObject => rendererObject;
    public EntityAnimationController AnimationController { get; protected set; }
    public EntityPhysicsController PhysicsController { get; protected set; }
    public EntityMovementController MovementController { get; protected set; }
    public EntityCombatController CombatController { get; protected set; }
    public StateFactory StateFactory { get; protected set; }
    public StateMachine StateMachine { get; protected set; }
    public StatusEffectManager StatusEffectManager { get; protected set; }
    public BarStatSystem HealthSystem { get; protected set; }
    private Vector2 direction;
    public Vector2 Direction {
        get
        {
            if(direction == Vector2.zero)
            {
                return rendererObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            }
            return direction;
        }
        set 
        {
            direction = value;
            if (direction.x > 0)
            {
                rendererObject.transform.localScale = defaultSpriteDirectionIsRight ? rightLocalScale : leftLocalScale;
            }
            else if (direction.x < 0)
            {
                rendererObject.transform.localScale = defaultSpriteDirectionIsRight ? leftLocalScale : rightLocalScale;
            }
        }
    }
    public bool IsAlive { get; protected set; }
    #endregion

    #region Private Properties

    private bool isSetUp = false;
    private static Vector3 leftLocalScale = new Vector3(-1, 1, 1);
    private static Vector3 rightLocalScale = new Vector3(1, 1, 1);

    #endregion

    public virtual void EnableEntity()
    {
        if (!isSetUp) SetUp();
        IsAlive = true;
        MovementController.Reset();
        StatusEffectManager.ClearStatusEffects();
        PhysicsController.ActiveAllColliders(true);
        CombatController.Reset();
        HealthSystem.AddValue(HealthSystem.MaxValue);
        StateManager.Ins?.Register(this);
        gameObject.SetActive(true);
    }

    protected virtual void SetUp()
    {
        if (isSetUp) return;
        SetUpAnimationController();
        SetUpPhysicsController();
        SetUpMovementController();
        SetUpCombatController();
        SetUpStateFactory();
        SetUpStateMachine();
        SetUpProperty();
        SetUpBarSystem();
        SetUpStatusEffect();

        Init();
        gameObject.SetActive(true);
        isSetUp = true;
    }

    protected virtual void Init()
    {
        InitAnimationController();
        InitPhysicsController();
        InitMovementController();
        InitCombatController();
        InitStateFactory();
        InitStateMachine();
        InitProperty();
        InitBarSystem();
        InitStatusEffect();

        InitPhysics();
    }

    protected virtual void SetUpStateFactory()
    {

    }
    protected virtual void SetUpStateMachine()
    {
        StateMachine = new StateMachine();
    }
    protected virtual void SetUpAnimationController()
    {

    }
    protected virtual void SetUpPhysicsController()
    {

    }
    protected virtual void SetUpMovementController()
    {

    }
    protected virtual void SetUpCombatController()
    {

    }
    protected virtual void SetUpProperty()
    {

    }
    protected virtual void SetUpBarSystem()
    {
        HealthSystem = new BarStatSystem<Entity>(this);
    }
    protected virtual void SetUpStatusEffect()
    {
        StatusEffectManager = new StatusEffectManager();
    }

    protected virtual void InitStateFactory()
    {

    }
    protected virtual void InitStateMachine()
    {

    }
    protected virtual void InitAnimationController()
    {
        AnimationController.Init(animator);
    }
    protected virtual void InitPhysicsController()
    {
        PhysicsController.Init(mainCollider, hitBox);
    }
    protected virtual void InitMovementController()
    {
        MovementController.InitRigidbody(rb);
    }
    protected virtual void InitCombatController()
    {

    }
    protected virtual void InitProperty()
    {
        IsAlive = true;
        Direction = Vector2.left;
    }
    protected virtual void InitBarSystem()
    {
        HealthSystem.Init(100);
    }
    protected virtual void InitStatusEffect()
    {
        StatusEffectManager.Init(this);
        StatusEffectManager.ClearStatusEffects();
    }

    protected virtual void InitPhysics()
    {
        PhysicsController.ActiveAllColliders(true);
    }

    public virtual void PreDisableEntity()
    {
        StatusEffectManager.ClearStatusEffects();
        PhysicsController.ActiveAllColliders(false);
    }

    public virtual void DisableEntity()
    {
        StateManager.Ins?.Unregister(this);
    }

    public virtual void CustomUpdate(float deltaTime)
    {
        StateMachine.CurrentState.Update(deltaTime);
        StatusEffectManager.Update(deltaTime);
        CombatController.Update(deltaTime);
    }

    public virtual void CustomFixedUpdate(float fixedDeltaTime)
    {
        StateMachine.CurrentState.FixedUpdate(fixedDeltaTime);
    }

    public void TakeDamage(DamageData damageData)
    {
        if (StatusEffectManager.HasStatusEffect(StatusEffectType.Invincible)) return;

        switch(damageData.AttackType)
        {
            case AttackTypeEnum.LightAttack:
                TimeManager.Ins.DoHitstop(0.04f);
                MainCharacterCameraController.Ins.ShakeLightAttack();
                break;
            case AttackTypeEnum.HeavyAttack:
                TimeManager.Ins.DoHitstop(0.1f);
                MainCharacterCameraController.Ins.ShakeHeavyAttack();
                break;
        }

        if (StatusEffectManager.CanBeInterupt)
        {
            Vector2 direction = (transform.position - damageData.Source.transform.position).normalized;
            MovementController.ApplyForceReset(direction, damageData.AttackForce);
        }

        BeforeTakeDamage(damageData);

        HealthSystem.AddValue(-damageData.Damage);
        StatusEffectManager.AddStatusEffect(damageData.Type, damageData.Duration, damageData.Value);

        AfterTakeDamage();
    }

    protected virtual void BeforeTakeDamage(DamageData damageData)
    {

    }

    protected virtual void AfterTakeDamage()
    {
        IsAlive = HealthSystem.Value > 0;
    }

    public virtual void OnHealthValueUpdated()
    {

    }

    public void HandleStatusEffect(StatusEffectType type, bool isAdded)
    {
        if (IsAlive)
        {
            if (isAdded)
            {
                uIStatusEffectList.AddStatusEffect(type);
            }
            else
            {
                uIStatusEffectList.RemoveStatusEffect(type);
            }

            OnHandleStatusEffect(type, isAdded);
        }
        else
        {
            if(!isAdded)
            {
                uIStatusEffectList.RemoveStatusEffect(type);
            }
        }
    }

    protected virtual void OnHandleStatusEffect(StatusEffectType type, bool isAdded)
    {

    }
}
