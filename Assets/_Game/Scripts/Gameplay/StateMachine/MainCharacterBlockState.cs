using UnityEngine;

public class MainCharacterBlockState : EntityState<MainCharacter>
{
    public MainCharacterBlockState(MainCharacter character) : base(character)
    {

    }

    public override void Enter()
    {
        entity.AnimationController.PlayAnimation(CharacterAnimationState.Block, entity.MovementSpeed);
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
