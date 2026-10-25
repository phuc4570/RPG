using UnityEngine;

public class DummyDisappearState : CharacterDisappearState<Dummy>
{
    protected override float StateDuration => 1f;
    public DummyDisappearState(Dummy dummy) : base(dummy)
    {

    }
}
