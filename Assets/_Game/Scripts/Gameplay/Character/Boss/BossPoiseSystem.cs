using UnityEngine;

public class BossPoiseSystem : BarStatSystem<Boss>
{
    private const int poiseGain = 1;

    public float poiseGainInterval = 0.5f;
    private float poiseGainTimer = 0f;
    private bool isGainingPoise = false;

    public BossPoiseSystem(Boss boss) : base(boss)
    {

    }

    public void SetDuration(float duration)
    {
        poiseGainInterval = (poiseGain * duration) / MaxValue;
    }

    public override void CustomUpdate(float deltaTime)
    {
        base.CustomUpdate(deltaTime);
        if (isGainingPoise && PercentageValue < 1f)
        {
            poiseGainTimer += deltaTime;
            if (poiseGainTimer >= poiseGainInterval)
            {
                poiseGainTimer -= poiseGainInterval;
                AddValue(poiseGain);
            }
        }
    }

    protected override void OnUpdateValue()
    {
        GameEvent.RaiseUpdateBossPoiseBar(PercentageValue);
    }
}
