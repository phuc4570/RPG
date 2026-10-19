using UnityEngine;

public class Dummy : Enemy
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new DummyStateFactory(this);
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(1000);
    }
}
