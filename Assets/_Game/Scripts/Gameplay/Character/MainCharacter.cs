using Sirenix.OdinInspector;
using UnityEngine;

public class MainCharacter : Character
{
    #region References
    [SerializeField] private MainCharacterInputReader mainCharacterInputReader;
    #endregion

    #region Public Properties
    public MainCharacterInputReader MainCharacterInputReader => mainCharacterInputReader;
    public MainCharacterMovementController MainCharacterMovementController => MovementController as MainCharacterMovementController;
    public bool IsBlocking { get; set; }
    public float DodgeForce { get; set; }
    public MainCharacterManaSystem ManaSystem { get; private set; }
    public MainCharacterStaminaSystem StaminaSystem { get; private set; }
    public MainCharacterFocusSystem FocusSystem { get; private set; }
    public MainCharacterHealingPotion HealingPotion { get; private set; }
    #endregion

    protected override void SetUpStateFactory()
    {
        StateFactory = new MainCharacterStateFactory(this);
    }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new MainCharacterHealthSystem(this);
        ManaSystem = new MainCharacterManaSystem(this);
        StaminaSystem = new MainCharacterStaminaSystem(this);
        FocusSystem = new MainCharacterFocusSystem();
        HealingPotion = new MainCharacterHealingPotion(this);
    }

    protected override void SetUpMovementController()
    {
        MovementController = new MainCharacterMovementController(this);
    }

    protected override void InitProperty()
    {
        BaseMovementSpeed = 5f;
        Direction = Vector2.right;
        IsBlocking = false;
        DodgeForce = 5f;

        BaseAttackDamage = 10;
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(100);
        ManaSystem.Init(60);
        StaminaSystem.Init(100);
        HealingPotion.Init(3, 30);
    }

    public override void DisableEntity()
    {
        base.DisableEntity();
    }

    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);
        StaminaSystem.CustomUpdate(deltaTime);
        ManaSystem.CustomUpdate(deltaTime);
    }
}
