using UnityEngine;

public class DummyHealingState : CharacterHealingState<Dummy>
{
    protected override float StateDuration => 2f;
    public DummyHealingState(Dummy dummy) : base(dummy)
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
