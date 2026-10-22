using UnityEngine;

public class MainCharacterDisappearState : CharacterDisappearState<MainCharacter>
{
    protected override float StateDuration => 1f;
    public MainCharacterDisappearState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }
}
