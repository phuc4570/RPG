using System;
using UnityEngine;

public static class GameEvent
{

    #region Gameplay Events

    public static event Action OnOneSecondPassed;
    public static event Action OnGameStart;

    #endregion

    #region UI Events



    #endregion

    #region Gameplay Raise Methods

    public static void RaiseOneSecondPassed() => OnOneSecondPassed?.Invoke();
    public static void RaiseGameStart() => OnGameStart?.Invoke();

    #endregion

    #region UI Raise Methods



    #endregion
}