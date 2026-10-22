using UnityEngine;

public class SuperDummyDisappearState : CharacterDisappearState<SuperDummy>
{
    protected override float StateDuration => 1f;
    public SuperDummyDisappearState(SuperDummy superDummy) : base(superDummy)
    {

    }
}
