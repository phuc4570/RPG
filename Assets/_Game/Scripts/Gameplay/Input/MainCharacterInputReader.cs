using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MainCharacterInputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    public Vector2 MoveInput { get; private set; }
    public Action<bool> OnBlockPerformed;
    public Action OnDodgePerformed;

    private InputAction moveAction;
    private InputAction blockAction;
    private InputAction dodgeAction;

    private void Awake()
    {
        InputActionMap inputActionMap = inputActionAsset.actionMaps[0];
        if (inputActionMap == null) return;
        moveAction = inputActionMap.FindAction(InputActionPropertyConfig.Move);
        if (moveAction != null)
        {
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
        }

        blockAction = inputActionMap.FindAction(InputActionPropertyConfig.Block);
        if (blockAction != null)
        {
            blockAction.performed += OnBlock;
            blockAction.canceled += OnBlock;
        }

        dodgeAction = inputActionMap.FindAction(InputActionPropertyConfig.Dodge);
        if (dodgeAction != null)
        {
            dodgeAction.performed += OnDodge;
        }
    }

    private void OnMove(CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnBlock(CallbackContext ctx) => OnBlockPerformed?.Invoke(ctx.ReadValueAsButton());
    private void OnDodge(CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            OnDodgePerformed?.Invoke();
        }
    }
}
