using UnityEngine;

public class MushroomDeadState : CharacterDeadState<Mushroom>
{
    protected override float StateDuration => 1f;
    public MushroomDeadState(Mushroom mushroom) : base(mushroom)
    {

    }
}
