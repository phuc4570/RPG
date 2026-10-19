using System;
using UnityEngine;

public class MainCharacterController : SingletonObject<MainCharacterController>
{
    [SerializeField] private MainCharacter mainCharacter;

    private void Start()
    {
        SetUpMainCharacter();
    }

    private void SetUpMainCharacter()
    {
        mainCharacter.SetUp();
    }

    public void ActiveMainCharacter(bool flag)
    {
        mainCharacter.gameObject.SetActive(flag);
    }
}
