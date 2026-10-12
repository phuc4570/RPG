using Sirenix.OdinInspector;
using UnityEngine;

public class MainCharacter : Character
{
    #region References
    [SerializeField] private MainCharacterInputReader mainCharacterInputReader;
    [SerializeField] private MainCharacterMovementController mainCharacterMovementController;
    [SerializeField] private MainCharacterFocusSystem mainCharacterFocusSystem;
    #endregion

    #region Public Properties
    public MainCharacterInputReader MainCharacterInputReader => mainCharacterInputReader;
    public MainCharacterMovementController MainCharacterMovementController => mainCharacterMovementController;
    public MainCharacterFocusSystem MainCharacterFocusSystem => mainCharacterFocusSystem;
    public bool IsBlocking { get; set; }
    public float DodgeForce { get; set; }
    #endregion

    protected override void SetUpStateFactory()
    {
        StateFactory = new MainCharacterStateFactory(this);
        StateMachine.Initialize(StateFactory.GetState(CharacterStateType.Idle));
    }

    public override void SetUpProperty()
    {
        BaseMovementSpeed = 5f;
        Direction = Vector2.right;
        IsBlocking = false;
        DodgeForce = 5f;
    }

    [Button("Change State")]
    public void ChangeState(CharacterStateType stateType)
    {
        StateMachine.ChangeState(StateFactory.GetState(stateType));
    }
}
