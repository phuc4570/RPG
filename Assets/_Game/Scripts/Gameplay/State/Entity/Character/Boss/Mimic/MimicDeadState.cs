using UnityEngine;

public class MimicDeadState : CharacterDeadState<Mimic>
{
    protected override float StateDuration => 0.5f;
    public MimicDeadState(Mimic mimic) : base(mimic)
    {

    }
}