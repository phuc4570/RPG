using UnityEngine;

public class SuperDummyHurtState : CharacterHurtState<SuperDummy>
{
    protected override float StateDuration => 0.2f;
    public SuperDummyHurtState(SuperDummy superDummy) : base(superDummy)
    {

    }
}