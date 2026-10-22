using System;
using UnityEngine;

public static class GameEvent
{

    #region Gameplay Events

    public static event Action OnOneSecondPassed;
    public static event Action OnGameStart;
    public static event Action<BossConfig> OnTriggerBossFight;

    #endregion

    #region UI Events

    public static event Action<float> OnUpdateBossHealthBar;
    public static event Action<float> OnUpdateBossPoiseBar;
    public static event Action<float> OnUpdateMainCharacterHealingPotion;
    public static event Action<float> OnUpdateMainCharacterHealthBar;
    public static event Action<float> OnUpdateMainCharacterManaBar;
    public static event Action<float> OnUpdateMainCharacterStaminaBar;
    public static event Action<float> OnVisualAttackFillAnim;
    public static event Action<float> OnVisualFocusFillAnim;

    #endregion

    #region Gameplay Raise Methods

    public static void RaiseOneSecondPassed() => OnOneSecondPassed?.Invoke();
    public static void RaiseGameStart() => OnGameStart?.Invoke();
    public static void RaiseTriggerBossFight(BossConfig bossConfig) => OnTriggerBossFight?.Invoke(bossConfig);

    #endregion

    #region UI Raise Methods

    public static void RaiseUpdateBossHealthBar(float value) => OnUpdateBossHealthBar?.Invoke(value);
    public static void RaiseUpdateBossPoiseBar(float value) => OnUpdateBossPoiseBar?.Invoke(value);
    public static void RaiseVisualAttackFillAnim(float duration) => OnVisualAttackFillAnim?.Invoke(duration);
    public static void RaiseVisualFocusFillAnim(float focusFill) => OnVisualFocusFillAnim?.Invoke(focusFill);
    public static void RaiseUpdateMainCharacterHealingPotion(float value) => OnUpdateMainCharacterHealingPotion?.Invoke(value);
    public static void RaiseUpdateMainCharacterHealthBar(float value) => OnUpdateMainCharacterHealthBar?.Invoke(value);
    public static void RaiseUpdateMainCharacterManaBar(float value) => OnUpdateMainCharacterManaBar?.Invoke(value);
    public static void RaiseUpdateMainCharacterStaminaBar(float value) => OnUpdateMainCharacterStaminaBar?.Invoke(value);

    #endregion
}