using UnityEngine;

public class MimicHurtState : CharacterHurtState<Mimic>
{
    protected override float StateDuration => 0.25f;

    public MimicHurtState(Mimic mimic) : base(mimic)
    {

    }
}