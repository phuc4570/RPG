using UnityEngine;

public class SuperDummy : Boss
{
    protected override void InitBarSystem()
    {
        HealthSystem.Init(10000);
    }
}
