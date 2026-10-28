using UnityEngine;

public class SuperDummyHealingState : CharacterHealingState<SuperDummy>
{
    protected override float StateDuration => 2f;
    public SuperDummyHealingState(SuperDummy superDummy) : base(superDummy)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.StatusEffectManager.AddStatusEffect(StatusEffectType.Invincible, StateDuration);
    }

    public override void Exit()
    {
        base.Exit();
        entity.EnableEntity();
    }
}