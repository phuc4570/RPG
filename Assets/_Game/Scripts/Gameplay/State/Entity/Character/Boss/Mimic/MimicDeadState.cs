using UnityEngine;

public class MimicDeadState : CharacterDeadState<Mimic>
{
    protected override float StateDuration => 1f;
    public MimicDeadState(Mimic mimic) : base(mimic)
    {

    }
}