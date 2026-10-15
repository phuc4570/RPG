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
    public Action OnLightAttackPerformed;
    public Action OnHeavyAttackCharged;
    public Action OnHeavyAttackPerformed;
    public Action OnHealPerformed;

    public bool IsBlocking { get; private set; }

    private InputAction moveAction;
    private InputAction blockAction;
    private InputAction dodgeAction;
    private InputAction attackAction;
    private InputAction healAction;

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

        attackAction = inputActionMap.FindAction(InputActionPropertyConfig.Attack);
        if (attackAction != null)
        {
            attackAction.performed += OnAttackPerformed;
            attackAction.started += OnAttackStarted;
            attackAction.canceled += OnAttackCanceled;
        }

        healAction = inputActionMap.FindAction(InputActionPropertyConfig.Heal);
        if (healAction != null)
        {
            healAction.performed += OnHeal;
        }
    }

    private void OnMove(CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnBlock(CallbackContext ctx)
    {
        IsBlocking = ctx.ReadValueAsButton();
        OnBlockPerformed?.Invoke(IsBlocking);
    }

    private void OnDodge(CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            OnDodgePerformed?.Invoke();
        }
    }

    private void OnAttackPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is UnityEngine.InputSystem.Interactions.TapInteraction)
        {
            OnLightAttackPerformed?.Invoke();
        }
    }

    private void OnAttackStarted(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            OnHeavyAttackCharged?.Invoke();
        }
    }

    private void OnAttackCanceled(InputAction.CallbackContext ctx)
    {
        if (ctx.interaction is UnityEngine.InputSystem.Interactions.HoldInteraction)
        {
            OnHeavyAttackPerformed?.Invoke();
        }
    }

    private void OnHeal(CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            OnHealPerformed?.Invoke();
        }
    }
}
