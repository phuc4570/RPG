using Sirenix.OdinInspector;
using UnityEngine;

public class MainCharacter : Character
{
    #region References
    [SerializeField] private MainCharacterInputReader mainCharacterInputReader;
    [SerializeField] private MainCharacterMovementController mainCharacterMovementController;
    #endregion

    #region Public Properties
    public MainCharacterInputReader MainCharacterInputReader => mainCharacterInputReader;
    public MainCharacterMovementController MainCharacterMovementController => mainCharacterMovementController;
    public bool IsBlocking { get; set; }
    public float DodgeForce { get; set; }
    public BarStatSystem ManaSystem { get; private set; }
    public BarStatSystem StaminaSystem { get; private set; }
    public MainCharacterFocusSystem FocusSystem { get; private set; }
    public MainCharacterHealingPotion HealingPotion { get; private set; }
    #endregion

    protected override void SetUpStateFactory()
    {
        StateFactory = new MainCharacterStateFactory(this);
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    protected override void SetUpBarSystem()
    {
        HealthSystem = new MainCharacterHealthSystem(100);
        ManaSystem = new MainCharacterManaSystem(100);
        StaminaSystem = new MainCharacterStaminaSystem(100);
        FocusSystem = new MainCharacterFocusSystem();
        HealingPotion = new MainCharacterHealingPotion(3, 30, this);
    }

    public override void SetUpProperty()
    {
        BaseMovementSpeed = 5f;
        Direction = Vector2.right;
        IsBlocking = false;
        DodgeForce = 5f;
        SetUpBarSystem();
    }

    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);
        StaminaSystem.CustomUpdate(deltaTime);
        ManaSystem.CustomUpdate(deltaTime);
    }

    [Button("Change State")]
    public void ChangeState(CharacterStateType stateType)
    {
        StateMachine.ChangeState(StateFactory.GetState(stateType));
    }
}
