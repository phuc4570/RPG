using UnityEngine;

public class MainCharacterHurtState : CharacterHurtState<MainCharacter>
{
    protected override float StateDuration => 0.25f;
    public MainCharacterHurtState(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, StateDuration);
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
