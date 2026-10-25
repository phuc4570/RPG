using UnityEngine;

public class DummyDeadState : CharacterDeadState<Dummy>
{
    protected override float StateDuration => 0.25f;
    public DummyDeadState(Dummy dummy) : base(dummy)
    {

    }
}
