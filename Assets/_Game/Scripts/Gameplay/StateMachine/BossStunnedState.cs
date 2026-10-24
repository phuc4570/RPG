using UnityEngine;

public class BossStunnedState : CharacterStunnedState<Boss>
{
    public BossStunnedState(Boss boss) : base(boss)
    {

    }

    public override void Enter()
    {
        base.Enter();
        entity.PoiseSystem.IsGainingPoise = true;
    }
}
