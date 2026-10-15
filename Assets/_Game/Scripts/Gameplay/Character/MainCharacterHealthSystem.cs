using UnityEngine;

public class MainCharacterHealthSystem : BarStatSystem
{
    public MainCharacterHealthSystem(int maxValue, int value = -1) : base(maxValue, value)
    {

    }

    public override void CustomUpdate(float deltaTime)
    {

    }

    protected override void OnUpdateMaxValue()
    {

    }

    protected override void OnUpdateValue()
    {
        GameEvent.RaiseUpdateMainCharacterHealthBar(PercentageValue);
    }

    protected override void OnValueIsOver()
    {

    }
    protected override void OnAddMaxValue(int value)
    {

    }

    protected override void OnAddValue(int value)
    {

    }
}
