using UnityEngine;

public abstract class Entity : MonoBehaviour, ITickable
{
    #region References
    [SerializeField] private GameObject rendererObject;
    [SerializeField] private EntityAnimationController animationController;
    #endregion

    #region Public Properties
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

    protected virtual void Awake()
    {
        StateMachine = new StateMachine();
        StatusEffectManager = new StatusEffectManager();
        SetUpStateFactory();
    }

    protected virtual void OnDestroy()
    {

    }

    protected virtual void OnEnable()
    {
        StateManager.Ins?.Register(this);
    }

    protected virtual void OnDisable()
    {
        StateManager.Ins?.Unregister(this);
    }

    protected abstract void SetUpStateFactory();
    protected abstract void SetUpBarSystem();
    public abstract void SetUpProperty();
    public abstract void SetUpStatusEffect();
    public abstract void SetUpAnimation();

    public virtual void CustomUpdate(float deltaTime)
    {
        StateMachine.CurrentState?.Update(deltaTime);
        StatusEffectManager.Update(deltaTime);
    }

    public virtual void CustomFixedUpdate(float fixedDeltaTime)
    {
        StateMachine.CurrentState.FixedUpdate(fixedDeltaTime);
    }
}
