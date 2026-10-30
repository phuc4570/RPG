using UnityEngine;

public class MimicTransformState : CharacterTransformState<Mimic>
{
    protected override float StateDuration => 0.66f;
    public MimicTransformState(Mimic character) : base(character)
    {

    }
}
