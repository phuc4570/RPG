using UnityEngine;

public class DummyHurtState : CharacterHurtState<Dummy>
{
    protected override float StateDuration => 0.2f;
    public DummyHurtState(Dummy dummy) : base(dummy)
    {

    }
}
