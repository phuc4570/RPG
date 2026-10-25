using UnityEngine;

public class MainCharacterHealthSystem : BarStatSystem<MainCharacter>
{
    public MainCharacterHealthSystem(MainCharacter mainCharacter) : base(mainCharacter)
    {

    }

    protected override void OnUpdateValue()
    {
        base.OnUpdateValue();
        GameEvent.RaiseUpdateMainCharacterHealthBar(PercentageValue);
    }
}
