using UnityEngine;

public class MainCharacterHurtState : CharacterHurtState<MainCharacter>
{
    public MainCharacterHurtState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override void Enter()
    {
        //entity.SetInvincible(true);
        //GameEvent.RaiseMainCharacterHurt();
    }

    public override void Update()
    {
        //if (entity.AnimationController.IsAnimationFinished())
        //{
        //    entity.StateMachine.ChangeState(new MainCharacterIdleState(entity));
        //}
    }

    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        //entity.SetInvincible(false);
    }
}
