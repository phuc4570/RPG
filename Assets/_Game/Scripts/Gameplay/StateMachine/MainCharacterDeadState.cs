using UnityEngine;

public class MainCharacterDeadState : CharacterDeadState<MainCharacter>
{
    public MainCharacterDeadState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //GameEvent.RaiseMainCharacterDead();
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
    }

    public override void FixedUpdate(float fixedDeltaTime)
    {
        base.FixedUpdate(fixedDeltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
