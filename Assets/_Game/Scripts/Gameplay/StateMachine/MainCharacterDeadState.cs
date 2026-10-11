using UnityEngine;

public class MainCharacterDeadState : CharacterDeadState<MainCharacter>
{
    public MainCharacterDeadState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override void Enter()
    {
        //GameEvent.RaiseMainCharacterDead();
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {

    }
}
