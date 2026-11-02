using UnityEngine;

public class Dummy : Enemy
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new DummyStateFactory(this);
    }

    public void AfterHealing()
    {
        PhysicsController.ActiveAllColliders(true);
        HealthSystem.AddValue(HealthSystem.MaxValue);
    }
}
