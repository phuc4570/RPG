using UnityEngine;

public class MushroomDisappearState : CharacterDisappearState<Mushroom>
{
    protected override float StateDuration => 1f;
    public MushroomDisappearState(Mushroom mushroom) : base(mushroom)
    {

    }
}
