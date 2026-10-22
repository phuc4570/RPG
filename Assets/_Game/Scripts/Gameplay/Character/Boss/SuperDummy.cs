using UnityEngine;

public class SuperDummy : Boss
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new SuperDummyStateFactory(this);
    }

    protected override void InitBarSystem()
    {
        HealthSystem.Init(100);
    }
}
