using UnityEngine;

public class MimicDisappearState : CharacterDisappearState<Mimic>
{
    protected override float StateDuration => 1f;
    public MimicDisappearState(Mimic mimic) : base(mimic)
    {

    }
}