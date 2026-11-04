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

    protected override void BeforeTakeDamage(DamageData damageData)
    {
        if (State == MimicState.Close)
        {
            damageData.SetDamage(Mathf.Clamp(Mathf.RoundToInt(damageData.Damage * BossCombatController.PoiseDamageReduction * 0.5f), 1, int.MaxValue));
        }
        else if (State == MimicState.Open)
        {
            damageData.SetDamage(Mathf.Clamp(Mathf.RoundToInt(damageData.Damage * BossCombatController.PoiseDamageReduction * 0.75f), 1, int.MaxValue));
        }
        else
        {
            if (PoiseSystem.Value > 0)
            {
                PoiseSystem.AddValue(-5);
                damageData.SetDamage(Mathf.Clamp(Mathf.RoundToInt(damageData.Damage * BossCombatController.PoiseDamageReduction), 1, int.MaxValue));
                if (PoiseSystem.PercentageValue == 0)
                {
                    StatusEffectManager.AddStatusEffect(StatusEffectType.Stunned, BossCombatController.PoiseStunDuration);
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
