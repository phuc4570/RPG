using Sirenix.OdinInspector;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class Entity : MonoBehaviour, ITickable, IDamageable
{
    #region References
    [SerializeField, Required] private SpriteRenderer rendererObject;
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
    public StateFactory StateFactory { get; protected set; }
    public StateMachine StateMachine { get; protected set; }
    public StatusEffectManager StatusEffectManager { get; protected set; }
    public BarStatSystem HealthSystem { get; protected set; }
    private Vector3 leftLocalScale = new Vector3(-1, 1, 1);
    private Vector3 rightLocalScale = new Vector3(1, 1, 1);
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
                rendererObject.transform.localScale = rightLocalScale;
            }
            else if (direction.x < 0)
            {
                rendererObject.transform.localScale = leftLocalScale;
            }
        }
    }
    public bool IsAlive { get; protected set; }
    public float BaseMovementSpeed { get; protected set; }
    public float MovementSpeed => BaseMovementSpeed;
    #endregion

    public virtual void SetUp()
    {
        SetUpAnimationController();
        SetUpPhysicsController();
        SetUpMovementController();
        SetUpStateFactory();
        SetUpStateMachine();
        SetUpProperty();
        SetUpBarSystem();
        SetUpStatusEffect();

        Init();
    }

    protected virtual void Init()
    {
        InitAnimationController();
        InitPhysicsController();
        InitMovementController();
        InitStateFactory();
        InitStateMachine();
        InitProperty();
        InitBarSystem();
        InitStatusEffect();

        InitPhysics();
    }

    protected virtual void OnEnable()
    {
        StateManager.Ins?.Register(this);
    }

    protected virtual void OnDisable()
    {
        StateManager.Ins?.Unregister(this);
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
        MovementController.Init(rb);
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

    public virtual void RespawnEntity()
    {
        IsAlive = true;
        StatusEffectManager.ClearStatusEffects();
        PhysicsController.ActiveAllColliders(true);
        HealthSystem.AddValue(HealthSystem.MaxValue);
    }

    public virtual void PreDisableEntity()
    {
        PhysicsController.ActiveAllColliders(false);
    }

    public virtual void DisableEntity()
    {

    }

    public virtual void CustomUpdate(float deltaTime)
    {
        StateMachine.CurrentState.Update(deltaTime);
        StatusEffectManager.Update(deltaTime);
    }

    public virtual void CustomFixedUpdate(float fixedDeltaTime)
    {
        StateMachine.CurrentState.FixedUpdate(fixedDeltaTime);
    }

    public void TakeDamage(int damage, StatusEffectType type = StatusEffectType.None, float duration = 0f, float value = 0f)
    {
        if (StatusEffectManager.HasStatusEffect(StatusEffectType.Invincible)) return;

        BeforeTakeDamage(ref damage);

        HealthSystem.AddValue(-damage);
        StatusEffectManager.AddStatusEffect(type, duration, value);

        AfterTakeDamage();
    }

    protected virtual void BeforeTakeDamage(ref int damage)
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
            //Handle Icon Visual Effect

            OnHandleStatusEffect(type, isAdded);
        }
    }

    protected virtual void OnHandleStatusEffect(StatusEffectType type, bool isAdded)
    {

    }
}
