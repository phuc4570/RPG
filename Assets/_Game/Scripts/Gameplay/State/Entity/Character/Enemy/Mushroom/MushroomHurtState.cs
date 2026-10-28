using UnityEngine;

public class MushroomHurtState : CharacterHurtState<Mushroom>
{
    protected override float StateDuration => 0.25f;

    public MushroomHurtState(Mushroom mushroom) : base(mushroom)
    {

    }
}
