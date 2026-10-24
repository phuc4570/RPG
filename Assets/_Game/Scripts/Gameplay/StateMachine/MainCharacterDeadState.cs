using UnityEngine;

public class MainCharacterDeadState : CharacterDeadState<MainCharacter>
{
    protected override float StateDuration => 0.5f;
    public MainCharacterDeadState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }
}
