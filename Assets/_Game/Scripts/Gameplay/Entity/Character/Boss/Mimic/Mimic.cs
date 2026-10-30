using UnityEngine;

public class Mimic : Boss
{
    public MimicState State { get; private set; }

    public override void EnableEntity()
    {
        base.EnableEntity();
        State = MimicState.Close;
    }

    protected override void SetUpStateFactory()
    {
        StateFactory = new MimicStateFactory(this);
    }

    protected override void InitProperty()
    {
        base.InitProperty();
        State = MimicState.Close;
    }

    protected override void BeforeTakeDamage(ref int damage)
    {
        if (State == MimicState.Close)
        {
            damage = Mathf.RoundToInt(damage * poiseDamageReduction);
            if (damage < 1) damage = 1;
        }
        else if (State == MimicState.Open)
        {
            damage = Mathf.RoundToInt(damage * poiseDamageReduction * 2);
            if (damage < 1) damage = 1;
        }
        else
        {
            if (PoiseSystem.Value > 0)
            {
                PoiseSystem.AddValue(-5);
                damage = Mathf.RoundToInt(damage * poiseDamageReduction);
                if (damage < 1) damage = 1;
                if (PoiseSystem.PercentageValue == 0)
                {
                    StatusEffectManager.AddStatusEffect(StatusEffectType.Stunned, poiseStunDuration);
                }
            }
        }
    }

    protected override void AfterTakeDamage()
    {
        base.AfterTakeDamage();
        if (State == MimicState.Close && HealthSystem.PercentageValue <= 0.95f)
        {
            State = MimicState.Open;
        }
        else if(State == MimicState.Open && HealthSystem.PercentageValue <= 0.9f)
        {
            State = MimicState.Transformed;
            StateMachine.ChangeState(StateFactory.GetState(CharacterStateType.Transform));
        }
    }

    public enum MimicState
    {
        Close,
        Open,
        Transformed,
    }
}
