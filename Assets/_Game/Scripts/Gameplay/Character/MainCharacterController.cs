using System;
using UnityEngine;

public class MainCharacterController : SingletonObject<MainCharacterController>
{
    [SerializeField] private MainCharacter mainCharacter;

    protected override void Awake()
    {
        SetUpMainCharacter();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void SetUpMainCharacter()
    {
        mainCharacter.SetUpProperty();
        mainCharacter.SetUpAnimation();
    }
}
