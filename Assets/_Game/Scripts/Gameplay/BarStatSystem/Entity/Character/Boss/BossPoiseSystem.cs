using UnityEngine;

public class BossPoiseSystem : BarStatSystem<Boss>
{

    public BossPoiseSystem(Boss boss) : base(boss)
    {

    }

    protected override void OnUpdateValue()
    {
        GameEvent.RaiseUpdateBossPoiseBar(PercentageValue);
    }
}
