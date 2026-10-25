using UnityEngine;

public class MainCharacterController : SingletonObject<MainCharacterController>
{
    [SerializeField] private MainCharacter mainCharacter;
    public MainCharacter MainCharacter => mainCharacter;

    public void ActiveMainCharacter(bool flag)
    {
        mainCharacter.gameObject.SetActive(flag);
        SetUpMainCharacter();
    }

    private void SetUpMainCharacter()
    {
        mainCharacter.SetUp();
    }

}
