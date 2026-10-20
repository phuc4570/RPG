using UnityEngine;

public abstract class Entity : MonoBehaviour, ITickable, IDamageable
{
    #region References
    [SerializeField] private SpriteRenderer rendererObject;
    [SerializeField] private EntityAnimationController animationController;
    #endregion

    #region Public Properties
    public SpriteRenderer RendererObject => rendererObject;
    public EntityAnimationController AnimationController => animationController;
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
    public float BaseMovementSpeed { get; protected set; }
    public float MovementSpeed => BaseMovementSpeed;
    #endregion

    public virtual void SetUp()
    {
        SetUpStateFactory();
        SetUpStateMachine();
        SetUpProperty();
        SetUpBarSystem();
        SetUpStatusEffect();

        Init();
    }

    protected virtual void Init()
    {
        InitStateFactory();
        InitStateMachine();
        InitProperty();
        InitBarSystem();
        InitStatusEffect();
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
    protected virtual void InitProperty()
    {
        BaseMovementSpeed = 0f;
        Direction = Vector2.left;
    }
    protected virtual void InitBarSystem()
    {
        HealthSystem.Init(100);
    }
    protected virtual void InitStatusEffect()
    {
        StatusEffectManager.ClearStatusEffects();
    }

    public virtual void DisableEntity()
    {

    }

    public virtual void CustomUpdate(float deltaTime)
    {
        StateMachine.CurrentState?.Update(deltaTime);
        StatusEffectManager.Update(deltaTime);
    }

    public virtual void CustomFixedUpdate(float fixedDeltaTime)
    {
        StateMachine.CurrentState.FixedUpdate(fixedDeltaTime);
    }

    public void TakeDamage(int damage, StatusEffectType type = StatusEffectType.None, float duration = 0f, float value = 0f)
    {
        if (StatusEffectManager.HasStatusEffect(StatusEffectType.Invincible)) return;

        HealthSystem.AddValue(-damage);
        StatusEffectManager.AddStatusEffect(type, duration, value);

        OnTakeDamage();
    }

    protected virtual void OnTakeDamage()
    {

    }

    public virtual void OnHealthValueUpdated()
    {

    }
}
