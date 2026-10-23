using UnityEngine;

public class Dummy : Enemy
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new DummyStateFactory(this);
    }
}
