using System;
using UnityEngine;

public static class GameEvent
{

    #region Gameplay Events

    public static event Action OnOneSecondPassed;

    #endregion

    #region UI Events



    #endregion

    #region Gameplay Raise Methods

    public static void RaiseOneSecondPassed() => OnOneSecondPassed?.Invoke();

    #endregion

    #region UI Raise Methods



    #endregion
}