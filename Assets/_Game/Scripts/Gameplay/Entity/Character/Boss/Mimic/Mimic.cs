using UnityEngine;

public class Mimic : Boss
{
    protected override void SetUpStateFactory()
    {
        StateFactory = new MimicStateFactory(this);
    }
}
